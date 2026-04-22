using AutoMapper;
using AymanStore.BLL.Interfaces;
using AymanStore.BLL.Repositories;
using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Entities;
using AymanStore.PL.Models;
using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AymanStore.PL.Controllers
{
    [Authorize(Roles = "Supplier, Admin")]
    public class SupplierController : Controller
    {
        private readonly ILogger<SupplierController> logger;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;
        private readonly IConfiguration configuration;

        public SupplierController(ILogger<SupplierController> logger, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            Mapper = mapper;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUser();

            var IncomeFromCompletedOrders = unitOfWork.InvoiceOrderDetailTBLRepository.GetAllCustomized(
                    filter: a => !a.IsDeleted && a.OrderDetailTBL.ProductTBL.SupplierTBLId == user.Id,
                                    includes: new Expression<Func<InvoiceOrderDetailTBL, object>>[]
                {
                        p => p.OrderDetailTBL,
                        p => p.OrderDetailTBL.ProductTBL
                }).ToList();

            var AllSupplierOrders = unitOfWork.OrderDetailTBLRepository.GetAllCustomized(
                    filter: a => !a.IsDeleted && a.ProductTBL.SupplierTBLId == user.Id && a.OrderTBL.DateOfSubmit != null,
                                    includes: new Expression<Func<OrderDetailTBL, object>>[]
                {
                        p => p.OrderTBL,
                        p => p.ProductTBL
                }).ToList();
            var model = new SupplierDashboard_VM
            {
                SupplierName = user.FirstName + " " + user.LastName,

                TotalProducts = unitOfWork.ProductTBLRepository
                    .GetAllCustomized(a => !a.IsDeleted && a.SupplierTBLId == user.Id).Count(),

                TotalOrders = AllSupplierOrders.Where(a => a.OrderTBL.DateOfSubmit != null).Count(),

                PendingOrders = AllSupplierOrders.Where(a => a.ShippingStatusTBLId == 1 && a.OrderTBL.DateOfSubmit != null).Count(),

                CompletedOrders = AllSupplierOrders.Where(a => a.ShippingStatusTBLId == 4 && a.OrderTBL.DateOfSubmit != null).Count(),

                CanceledOrders = AllSupplierOrders.Where(a => a.ShippingStatusTBLId == 5 && a.OrderTBL.DateOfSubmit != null).Count(),

                ExpectedOrders = AllSupplierOrders.Where(a => a.ShippingStatusTBLId == 1 && a.OrderTBL.DateOfSubmit == null).Count(),

                ReviewsOrders = AllSupplierOrders.Where(a => a.IsUserWroteReview && a.OrderTBL.DateOfSubmit != null).Count(),

                ComplainProducts = unitOfWork.ContactForProductErrorTBLRepository
                    .GetAllCustomized(a => !a.IsDeleted && a.ProductTBL.SupplierTBLId == user.Id).Count(),

                ComplainProductsNew = unitOfWork.ContactForProductErrorTBLRepository
                    .GetAllCustomized(a => !a.IsDeleted && a.ProductTBL.SupplierTBLId == user.Id && string.IsNullOrEmpty(a.MessageReplyToClient)).Count(),

                TotalIncome = IncomeFromCompletedOrders.Sum(a => a.SupplierAmount ?? 0),

                CashBalance = IncomeFromCompletedOrders.Where(a => !a.IsSupplierReceivedMoney).Sum(a => a.SupplierAmount ?? 0)
            };

            return View(model);
        }

        #region SupplierOrders & Admin
        [HttpGet]
        public async Task<IActionResult> SupplierOrders(string? barcode)
        {
            ViewBag.CurrentUser = "Supplier";
            var user = await GetCurrentUser();
            bool supplier = unitOfWork.UserManager.IsInRoleAsync(user, "Supplier").Result;
            if (supplier)
                return View(await GetOrderDetailsForSupplier(barcode, user.Id));

            ViewBag.CurrentUser = "Admin";
            return View(await GetOrderDetailsForSupplier(barcode, null));

        }

        [HttpPost]
        public IActionResult UpdateTrackingCode(int id, string trackingCode)
        {
            var item = unitOfWork.OrderDetailTBLRepository.GetById(id);

            if (item == null)
                return Json(new { success = false });

            item.ShippingCompanyTRKCode = trackingCode;

            unitOfWork.OrderDetailTBLRepository.Update(item);

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderDetailStatus(int orderDetailId, int statusId)
        {
            var orderDetail = unitOfWork.OrderDetailTBLRepository.GetById(orderDetailId);
            if (orderDetail == null) return Json(new { success = false });

            orderDetail.ShippingStatusTBLId = statusId;
            orderDetail.LastUpdateDate = DateTime.Now;
            unitOfWork.OrderDetailTBLRepository.Update(orderDetail);

            // --- Update parent order status ---
            var order = unitOfWork.OrderTBLRepository.GetAllCustomized(
                filter: a => !a.IsDeleted && a.ID == orderDetail.OrderTBLId)
                .FirstOrDefault();

            if (order != null)
            {
                var orderDetails = unitOfWork.OrderDetailTBLRepository.GetAllCustomized(
                    filter: a => !a.IsDeleted && a.OrderTBLId == order.ID,
                                    includes: new Expression<Func<OrderDetailTBL, object>>[]
                {
                        p => p.ProductTBL,
                        p => p.ShippingStatusTBL
                })
                    .ToList();
                List<ConfirmationOrder_VM> items = new List<ConfirmationOrder_VM>();
                items = orderDetails.Where(a => a.ShippingStatusTBLId != 5).Select(item => new ConfirmationOrder_VM
                {
                    ItemName = item.ProductTBL?.Name ?? "Unknown Product",
                    Barcode = item.ProductTBL?.Barcode ?? "N/A",
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Shipping = item.Quantity * item.ShippingCost,
                    ShippingStatus = item.ShippingStatusTBL?.Name ?? "Unknown Status"

                }).ToList();
                if (statusId == 4)
                {
                    var getInvoiceRate = unitOfWork.InvoiceRateTBLRepository.GetAllCustomized(
                    filter: a => !a.IsDeleted).FirstOrDefault();

                    var TotalAmount = (int)(orderDetail.Price * orderDetail.Quantity);
                    var VATAmount = TotalAmount * getInvoiceRate?.VATRate / 100;
                    var ProfitAmount = TotalAmount * getInvoiceRate?.ProfitRate / 100;
                    var ShippingAmount = (int)(orderDetail.ShippingCost * orderDetail.Quantity);
                    var SupplierAmount = TotalAmount - VATAmount - ProfitAmount;
                    var invoiceOrderDetail = new InvoiceOrderDetailTBL
                    {
                        OrderDetailTBLId = orderDetail.ID,
                        VATAmount = VATAmount,
                        ProfitAmount = ProfitAmount,
                        ShippingAmount = ShippingAmount,
                        SupplierAmount = SupplierAmount
                    };
                    unitOfWork.InvoiceOrderDetailTBLRepository.Add(invoiceOrderDetail);
                }
                else if (statusId == 5)
                {
                    // Calculate totals
                    int OrderDetailAndQuantity = items.Sum(i => (int)(i.Price * i.Quantity));
                    int OrderDetailShipping = items.Sum(i => (int)(i.Shipping * i.Quantity));

                    // Calculate Total Amount
                    order.TotalAmount = OrderDetailAndQuantity + OrderDetailShipping;
                    order.LastUpdateDate = DateTime.Now;
                    unitOfWork.OrderTBLRepository.Update(order);
                }
                try
                {
                    // Sending Email               
                    var ActivateLink = configuration["AymanStore.Pl.Url"] + "User/OrderDetail?orderId=" + order.ID;

                    var Email = new EmailTBL_VM();
                    Email.To = order.ClientEmail;
                    Email.Subject = configuration["AymanStore.Pl.Name"] + " - Order Updates";
                    Email.Body = await GetOrderConfirmationTemplateAsync(order.ClientName ?? "", order.HashCode ?? "", (DateTime)order.DateOfSubmit, BuildOrderItemsTable(items), order.TotalAmount.ToString() ?? "", ActivateLink, order.TrackingShippingUrl);
                    var newEmail = Mapper.Map<EmailTBL>(Email);

                    // Send email
                    var supplierEmails = orderDetails.Where(a => a.ShippingStatusTBLId != 5)
                        .Where(x => !string.IsNullOrEmpty(x.SupplierEmail))
                        .Select(x => x.SupplierEmail.Trim())
                        .Distinct()
                        .ToList();

                    await unitOfWork.EmailTBLRepository.SendEmailAsync(newEmail, 2, supplierEmails);                        // Save Email
                    unitOfWork.EmailTBLRepository.Add(newEmail);
                }
                catch (Exception ex)
                {
                    unitOfWork.AppErrorTBLRepository.Add(new AppErrorTBL
                    {
                        Message = ex.Message,
                        StackTrace = ex.StackTrace ?? "",
                        Controller = nameof(SupplierController),
                        Action = nameof(UpdateOrderDetailStatus)
                    });
                }


                if (orderDetails.Any())
                {
                    // If all items have the same status, set order status to that
                    var firstStatus = orderDetails?.FirstOrDefault()?.ShippingStatusTBLId;
                    if (orderDetails.All(d => d.ShippingStatusTBLId == firstStatus))
                    {
                        order.ShippingStatusTBLId = firstStatus;
                        order.LastUpdateDate = DateTime.Now;
                        unitOfWork.OrderTBLRepository.Update(order);
                    }
                }
            }
            var statusName = unitOfWork.ShippingStatusTBLRepository.GetById(statusId)?.Name ?? "Unknown";

            return Json(new { success = true, statusText = statusName });
        }
        #endregion

        #region Helper Methods
        private async Task<AppUser> GetCurrentUser()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await unitOfWork.UserManager.FindByIdAsync(userId);
        }

        private async Task<SupplierOrders_VM> GetOrderDetailsForSupplier(string? barcode, string? userId)
        {
            var model = new SupplierOrders_VM();

            var orders = unitOfWork.OrderDetailTBLRepository.GetAllCustomized(
                filter: a =>
                    !a.IsDeleted &&
                    a.OrderTBL.IsСurrentOrder == false &&
                    a.OrderTBL.IsDeleted == false &&
                    (string.IsNullOrEmpty(userId) || a.ProductTBL.SupplierTBLId == userId) &&
                    (string.IsNullOrEmpty(barcode) || a.ProductTBL.Barcode.Contains(barcode)),
                includes: new Expression<Func<OrderDetailTBL, object>>[]
                {
            p => p.OrderTBL,
            p => p.OrderTBL.CountryTBL,
            p => p.ProductTBL,
            p => p.ShippingStatusTBL,
            p => p.ShippingCompanyCostTBL,
            p => p.ShippingCompanyCostTBL.ShippingCompanyTBL,
            p => p.ShippingCompanyCostTBL.CountryTBLSendFrom,
            p => p.ShippingCompanyCostTBL.CountryTBLSendTo,
            p => p.OrderTBL.ShippingServiceTBL,
                })
                .OrderByDescending(p => p.CreationDate)
                .ToList();

            model.OrderDetailTBL_VM = Mapper.Map<List<OrderDetailTBL_VM>>(orders);

            model.ShippingStatusOptions = unitOfWork.ShippingStatusTBLRepository
                .GetAllCustomized(a => !a.IsDeleted)
                .Select(s => new SelectListItem
                {
                    Value = s.ID.ToString(),
                    Text = s.Name
                }).ToList();

            return model;
        }
        

        private async Task<string> GetOrderConfirmationTemplateAsync(string firstName, string orderNumber, DateTime orderDate, string orderItemsHtml, string totalPrice, string orderLink, string trackingOrderURL)
        {
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "template2", "Order-Confirmation.html");
            string html = await System.IO.File.ReadAllTextAsync(templatePath);

            html = html.Replace("{{FirstName}}", firstName);
            html = html.Replace("{{OrderNumber}}", orderNumber);
            html = html.Replace("{{OrderDate}}", orderDate.ToString("MMMM dd, yyyy"));
            html = html.Replace("{{OrderItems}}", orderItemsHtml);
            html = html.Replace("{{TotalPrice}}", totalPrice);
            html = html.Replace("{{OrderLink}}", orderLink);
            html = html.Replace("{{TrackingOrderUrl}}", trackingOrderURL);
            html = html.Replace("{{Year}}", DateTime.Now.Year.ToString());

            return html;
        }

        private string BuildOrderItemsTable(List<ConfirmationOrder_VM> items)
        {
            if (items == null || items.Count == 0)
                return "<tr><th><td>No items in the order.</td></th></tr>";

            var sb = new StringBuilder();
            //sb.Append("<table style='width:100%; border-collapse:collapse;'>");
            //sb.Append("<tr><th>Item</th><th>Qty</th><th>Price</th><th>Shipping</th></tr>");

            foreach (var item in items)
            {
                sb.Append($@"
            <tr>
                <td>{item.ItemName}</td>
                <td>{item.Barcode}</td>
                <td>{item.Quantity}</td>
                <td>${item.Price:F2}</td>
                <td>${item.Shipping:F2}</td>
                <td>{item.ShippingStatus}</td>
            </tr>");
            }

            sb.Append("</table>");
            return sb.ToString();
        }
        #endregion


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
