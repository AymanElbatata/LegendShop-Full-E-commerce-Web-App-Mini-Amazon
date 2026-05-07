using AutoMapper;
using AymanStore.BLL.Interfaces;
using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Entities;
using AymanStore.PL.DTO;
using AymanStore.PL.Models;
using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AymanStore.PL.Controllers
{
    [Authorize(Roles = "User, Supplier, Admin")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> logger;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;
        private readonly IConfiguration configuration;

        public UserController(ILogger<UserController> logger, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            Mapper = mapper;
            this.logger = logger;
        }

        public async Task<IActionResult> Index(string? Message)
        {
            if (!string.IsNullOrEmpty(Message))
            {
                ViewBag.SuccessMessage = Message;
            }
            var currentUser = await GetCurrentUser();
            var model = new UserProfileDTO()
            {
                Email = currentUser.Email,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                Address = currentUser.Address,
                Phone = currentUser.PhoneNumber,
                CountryTBLId = currentUser.CountryTBLId,
                GenderTBLId = currentUser.GenderTBLId,
                CountryOptions = unitOfWork.CountryTBLRepository.GetAllCustomized(
             filter: a => a.IsDeleted == false)
            .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name, Selected = c.ID == currentUser.CountryTBLId })
            .ToList(),
                GenderOptions = unitOfWork.GenderTBLRepository.GetAllCustomized(
             filter: a => a.IsDeleted == false)
            .Select(g => new SelectListItem { Value = g.ID.ToString(), Text = g.Name, Selected = g.ID == currentUser.GenderTBLId })
            .ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserProfileDTO model)
        {
            if (!ModelState.IsValid)
            {
                var currentUser = await GetCurrentUser();
                currentUser.FirstName = model.FirstName;
                currentUser.LastName = model.LastName;
                currentUser.Address = model.Address;
                currentUser.PhoneNumber = model.Phone;
                currentUser.CountryTBLId = model.CountryTBLId;
                currentUser.GenderTBLId = model.GenderTBLId;
                await unitOfWork.UserManager.UpdateAsync(currentUser);
                return RedirectToAction("Index", "User", new { Message = "Profile updated successfully!" });
            }
            return View(model);
        }

        #region Report Abuse Product Rating
        [HttpGet]
        public IActionResult ReportAbuseProductRating(int productReviewId)
        {
            var model = new ReportAbuseProductRating_VM();
            var productRating = unitOfWork.ProductRatingTBLRepository.GetAllCustomized(
                            filter: a => a.IsDeleted == false && a.ID == productReviewId,
                    includes: new Expression<Func<ProductRatingTBL, object>>[]
                    {
                                      p => p.ProductTBL,
                    }).FirstOrDefault();
            if (productRating != null)
                model.ProductRatingTBL_VM = Mapper.Map<ProductRatingTBL_VM>(productRating);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReportAbuseProductRating(ReportAbuseProductRating_VM model)
        {
            unitOfWork.AbuseProductRatingTBLRepository.Add(new AbuseProductRatingTBL
            {
                ProductRatingTBLId = model.ProductRatingTBL_VM.ID,
                Message = model.AbuseProductRatingTBL_VM.Message,
                SenderUserTBLId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction("ProductDetail", "Home", new { ProductId = model.ProductRatingTBL_VM.ProductTBLId, IncomingMessage = "Your Report has been sent successfully!" });
        }
        #endregion

        #region User Review After Delivery
        [HttpPost]
        public async Task<IActionResult> SubmitProductReview(int orderDetailId, int productId, int stars, string subject, string comment, bool IsHelpful)
        {
            try
            {
                var user = await GetCurrentUser();
                if (user == null)
                    return Json(new { success = false, message = "User not found." });

                // Validate stars
                if (stars < 1 || stars > 5)
                    return Json(new { success = false, message = "Stars must be between 1 and 5." });

                // Validate required fields
                if (string.IsNullOrWhiteSpace(subject))
                    return Json(new { success = false, message = "Subject is required." });

                if (string.IsNullOrWhiteSpace(comment))
                    return Json(new { success = false, message = "Comment is required." });

                var orderDetail = unitOfWork.OrderDetailTBLRepository.GetById(orderDetailId);
                if (orderDetail == null)
                    return Json(new { success = false, message = "Order detail not found." });

                // Check if user already submitted review
                if (!orderDetail.IsUserWroteReview)
                {
                    var review = new ProductRatingTBL
                    {
                        ProductTBLId = productId,
                        AppUserWhoRatedId = user.Id,
                        Stars = stars,
                        ReviewSubject = subject,
                        ReviewComment = comment,
                        IsHelpful = IsHelpful,
                    };
                    unitOfWork.ProductRatingTBLRepository.Add(review);

                    orderDetail.IsUserWroteReview = true;
                    orderDetail.LastUpdateDate = DateTime.Now; // Update the last update date
                    unitOfWork.OrderDetailTBLRepository.Update(orderDetail);

                    return Json(new { success = true, message = "Review submitted successfully!" });
                }

                return Json(new { success = false, message = "You have already submitted a review for this product." });
            }
            catch (Exception ex)
            {
                // Log the exception
                return Json(new { success = false, message = "An error occurred while submitting your review." });
            }
        }
        #endregion

        #region Orders & Order Details
        public IActionResult Orders()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var data = new Orders_VM();

            var Orders = unitOfWork.OrderTBLRepository.GetAllCustomized(
                    filter: a => a.IsDeleted == false && a.OrderByUserTBLId == userId && a.IsСurrentOrder == false,
                    includes: new Expression<Func<OrderTBL, object>>[]
                    {
                                p => p.OrderByUserTBL,
                                p => p.CountryTBL,
                                p => p.ShippingServiceTBL,
                                p => p.ShippingStatusTBL,
                                //p => p.ShippingCompanyTBL

                   });
            if (Orders != null)
            {
                data.OrderTBL_VM = Mapper.Map<List<OrderTBL_VM>>(Orders.OrderByDescending(p => p.CreationDate));
                data.OrderDetailTBL_VM = Mapper.Map<List<OrderDetailTBL_VM>>(GetCurrentUserOrderDetails().OrderByDescending(p => p.CreationDate));
                return View(data);
            }
            return View(data);
        }

        public IActionResult OrderDetail(int? orderId)
        {
            var data = new ShoppingCart_VM();

            if (orderId > 0 || GetUserOrder(Convert.ToInt32(orderId)) != null)
            {
                var CurrentOrder = GetUserOrder(Convert.ToInt32(orderId));
                data.OrderTBL_VM = Mapper.Map<OrderTBL_VM>(CurrentOrder);
                data.OrderDetailTBL_VM = Mapper.Map<List<OrderDetailTBL_VM>>(GetCompletedUserOrderDetails(CurrentOrder.ID));
            }
            return View(data);
        }
        #endregion

        #region ShoppingCart + Add-Update-Delete
        public async Task<IActionResult> ShoppingCart(string? Message = null)
        {
            var data = new ShoppingCart_VM();
            if (!string.IsNullOrEmpty(Message))
            {
                ViewBag.NotAllowedMessage = Message;
            }
            await IfOrderCountryandUserCountry();
            var CurrentOrder = GetCurrentUserOrder();
            if (CurrentOrder == null && !await IfOrderCountryandUserCountry())
            {
                await AddNewOrderForCurrentUser();
                CurrentOrder = GetCurrentUserOrder();
            }
            data.OrderTBL_VM = Mapper.Map<OrderTBL_VM>(CurrentOrder);
            data.OrderDetailTBL_VM = Mapper.Map<List<OrderDetailTBL_VM>>(GetCurrentUserOrderDetails(CurrentOrder.ID));
            return View(data);
        }

        public async Task<IActionResult> AddToCart(int? productIdAddToCart, int? quantityAddToCart)
        {
            var currentProduct = unitOfWork.ProductTBLRepository.GetAllCustomized(
                       filter: a => a.IsDeleted == false && a.ID == productIdAddToCart,
                       includes: new Expression<Func<ProductTBL, object>>[]
                       {p => p.SupplierTBL }).FirstOrDefault() ?? null;
            if (currentProduct != null)
            {
                await IfOrderCountryandUserCountry();
                var CurrentOrder = GetCurrentUserOrder();
                if (CurrentOrder == null)
                {
                    await AddNewOrderForCurrentUser();
                    CurrentOrder = GetCurrentUserOrder();
                }
                if (!ISProductISAllowedToThisUser(Convert.ToInt32(productIdAddToCart), Convert.ToInt32(CurrentOrder.CountryTBLId)))
                    return RedirectToAction("ShoppingCart", "User", new { Message = "Currently this product is not available in your country!" });

                if (!GetCurrentUserOrderDetails(CurrentOrder.ID).Where(a => a.ProductTBLId == productIdAddToCart).Any())
                {
                    var shippingCompanyCost = GetShippingCompanyCost(Convert.ToInt32(productIdAddToCart), Convert.ToInt32(CurrentOrder.CountryTBLId));
                    var newOrder = new OrderDetailTBL();
                    newOrder.ProductTBLId = productIdAddToCart;
                    newOrder.Quantity = quantityAddToCart;
                    newOrder.Price = currentProduct?.SellingPrice;
                    newOrder.OrderTBLId = CurrentOrder.ID;
                    newOrder.ShippingStatusTBLId = 1;
                    newOrder.ShippingCompanyCostTBLId = shippingCompanyCost?.ID;
                    newOrder.ShippingCost = shippingCompanyCost?.Cost;
                    newOrder.SupplierAddress = currentProduct?.SupplierTBL?.Address;
                    newOrder.SupplierPhones = currentProduct?.SupplierTBL?.PhoneNumber;
                    newOrder.SupplierEmail = currentProduct?.SupplierTBL?.Email;
                    unitOfWork.OrderDetailTBLRepository.Add(newOrder);
                }

            }
            return RedirectToAction("ShoppingCart", "User");
        }


        public IActionResult UpdateQuantityCart(int? productId, int? productQuantity)
        {
            var CurrentOrder = GetCurrentUserOrder();
            if (CurrentOrder != null)
            {
                var UpdatedOrderDetail = GetCurrentUserOrderDetails(CurrentOrder.ID).Where(a => a.ProductTBLId == productId).FirstOrDefault();
                if (UpdatedOrderDetail != null)
                {
                    UpdatedOrderDetail.Quantity = productQuantity;
                    unitOfWork.OrderDetailTBLRepository.Update(UpdatedOrderDetail);
                }
            }
            return RedirectToAction("ShoppingCart", "User");
        }

        public IActionResult RemoveFromCart(int? productId)
        {

            var CurrentOrder = GetCurrentUserOrder();
            if (CurrentOrder != null)
            {
                var UpdatedOrderDetail = GetCurrentUserOrderDetails(CurrentOrder.ID).Where(a => a.ProductTBLId == productId).FirstOrDefault();
                if (UpdatedOrderDetail != null)
                {
                    UpdatedOrderDetail.IsDeleted = true;
                    unitOfWork.OrderDetailTBLRepository.Update(UpdatedOrderDetail);
                }
            }
            return RedirectToAction("ShoppingCart", "User");
        }
        #endregion

        #region CheckOut
        public async Task<IActionResult> CheckOutOrder()
        {
            var data = new CheckOut_VM();
            var CurrentOrder = GetCurrentUserOrder();
            var CurrentUser = await GetCurrentUser();
            if (CurrentOrder != null && !await IfOrderCountryandUserCountry())
            {
                if (GetCurrentUserOrderDetails(CurrentOrder.ID).Count < 1)
                    return RedirectToAction("ShoppingCart", "User", new { Message = "Please Add products to your cart first!" });

                data.OrderTBL_VM = Mapper.Map<OrderTBL_VM>(CurrentOrder);
                data.OrderTBL_VM.ClientAddress = CurrentUser.Address;
                data.OrderTBL_VM.ClientEmail = CurrentUser.Email;
                data.OrderTBL_VM.ClientPhones = CurrentUser.PhoneNumber;
                data.OrderTBL_VM.ClientName = CurrentUser.FirstName + " " + CurrentUser.LastName;
                int Totalcost;
                data.TotalItemsOfOrder = GetTotalItemsOfOrder(data.OrderTBL_VM.ID, out Totalcost);
                data.TotalAmountOfOrder = Totalcost;

                data.CountryTBL_VM = Mapper.Map<List<CountryTBL_VM>>(unitOfWork.CountryTBLRepository
                    .GetAllCustomized(filter: a => a.IsDeleted == false));
                data.ShippingServiceTBL_VM = Mapper.Map<List<ShippingServiceTBL_VM>>(unitOfWork.ShippingServiceTBLRepository
                     .GetAllCustomized(filter: a => a.IsDeleted == false));
                return View(data);
            }
            ModelState.AddModelError("", "Error, You changed your country, Please Add items again!");
            return View(data);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CheckOutOrder(CheckOut_VM model)
        {
            try
            {
                var CurrentOrder = GetCurrentUserOrder();
                if (CurrentOrder != null && !await IfOrderCountryandUserCountry())
                {
                    if (GetCurrentUserOrderDetails(CurrentOrder.ID).Count < 1)
                        return RedirectToAction("ShoppingCart", "User", new { Message = "Please Add Products first!" });

                    var OrderDetailsShipping = GetCurrentUserOrderDetails(CurrentOrder.ID);
                    var ShippingCompanyCostToId = OrderDetailsShipping.FirstOrDefault()?.ShippingCompanyCostTBL?.CountryTBLSendTo?.ID;
                    if (CurrentOrder.CountryTBLId != ShippingCompanyCostToId)
                    {
                        ModelState.AddModelError("", "One or more products cannot be shipped for you because the country of shipping, please select products can be shipped for your country!");
                        model.CountryTBL_VM = Mapper.Map<List<CountryTBL_VM>>(unitOfWork.CountryTBLRepository
                              .GetAllCustomized(filter: a => a.IsDeleted == false));
                        model.ShippingServiceTBL_VM = Mapper.Map<List<ShippingServiceTBL_VM>>(unitOfWork.ShippingServiceTBLRepository
                             .GetAllCustomized(filter: a => a.IsDeleted == false));
                        return View(model);
                    }

                    CurrentOrder.ClientAddress = model.OrderTBL_VM.ClientAddress;
                    CurrentOrder.ClientEmail = model.OrderTBL_VM.ClientEmail;
                    CurrentOrder.ClientPhones = model.OrderTBL_VM.ClientPhones;
                    CurrentOrder.ClientName = model.OrderTBL_VM.ClientName;
                    CurrentOrder.ClientNotes = model.OrderTBL_VM.ClientNotes;

                    CurrentOrder.ShippingStatusTBLId = 1;
                    CurrentOrder.ShippingServiceTBLId = model.OrderTBL_VM.ShippingServiceTBLId;
                    //CurrentOrder.CountryTBLId = model.OrderTBL_VM.CountryTBLId;
                    CurrentOrder.IsСurrentOrder = false;
                    CurrentOrder.DateOfSubmit = DateTime.Now;
                    CurrentOrder.TrackingShippingCode = "TRK" + unitOfWork.MySPECIALGUID.GetUniqueKey(10).ToUpper();
                    CurrentOrder.TrackingShippingUrl = configuration["AymanStore.Pl.Url"] + "Home/TrackingOrder?trackingCode=" + CurrentOrder.TrackingShippingCode;

                    List<ConfirmationOrder_VM> items = OrderDetailsShipping.Select(item => new ConfirmationOrder_VM
                    {
                        ItemName = item.ProductTBL?.Name ?? "Unknown Product",
                        Barcode = item.ProductTBL?.Barcode ?? "N/A",
                        Quantity = item.Quantity,
                        Price = item.Price,
                        Shipping = item.Quantity * item.ShippingCost,
                        ShippingStatus = item.ShippingStatusTBL?.Name ?? "Unknown Status"
                    }).ToList();

                    // Calculate totals
                    int OrderDetailAndQuantity = items.Sum(i => (int)(i.Price * i.Quantity));
                    int OrderDetailShipping = items.Sum(i => (int)i.Shipping);

                    // Calculate Total Amount
                    CurrentOrder.TotalAmount = OrderDetailAndQuantity + OrderDetailShipping;
                    unitOfWork.OrderTBLRepository.Update(CurrentOrder);

                    try
                    {
                        // Sending Email               
                        var ActivateLink = configuration["AymanStore.Pl.Url"] + "User/OrderDetail?orderId=" + CurrentOrder.ID;

                        var Email = new EmailTBL_VM();
                        Email.To = CurrentOrder.OrderByUserTBL.Email;
                        Email.Subject = configuration["AymanStore.Pl.Name"] + " - Order Confirmation";
                        Email.Body = await GetOrderConfirmationTemplateAsync(GetCurrentUser().Result.FirstName ?? "", CurrentOrder.HashCode ?? "", (DateTime)CurrentOrder.DateOfSubmit, BuildOrderItemsTable(items), CurrentOrder.TotalAmount.ToString() ?? "", ActivateLink, CurrentOrder.TrackingShippingUrl);
                        var newEmail = Mapper.Map<EmailTBL>(Email);

                        // Send email
                        var supplierEmails = OrderDetailsShipping
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
                            Controller = nameof(UserController),
                            Action = nameof(CheckOutOrder)
                        });
                    }

                    return RedirectToAction("Orders", "User");
                }
            }
            catch (Exception ex)
            {
                // Log to console / file
                logger.LogError(ex, "Error in {Controller}.{Action}", nameof(UserController), nameof(CheckOutOrder));

                // Save to DB
                unitOfWork.AppErrorTBLRepository.Add(new AppErrorTBL
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace ?? "",
                    Controller = nameof(UserController),
                    Action = nameof(CheckOutOrder)
                });
                System.IO.File.AppendAllText("C:\\Temp\\FallbackLog.txt", ex.ToString());

            }
            ModelState.AddModelError("", "Error, You changed your country, Please Add items again!");
            return View(model);
        }
        #endregion

        #region Product Complain & ProductSupport

        [HttpGet]
        public async Task<IActionResult> ContactForProductError(int productId)
        {
            var ContactForProductErrorTBL_VM = new ContactForProductErrorDTO();

            var Product = unitOfWork.ProductTBLRepository.GetAllCustomized(
                filter: a => a.IsDeleted == false && a.ID == productId).FirstOrDefault();

            ContactForProductErrorTBL_VM.ProductTBL_VM = Mapper.Map<ProductTBL_VM>(Product);

            return View(ContactForProductErrorTBL_VM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactForProductError(ContactForProductErrorDTO model)
        {
            if (ModelState.IsValid)
            {

                unitOfWork.ContactForProductErrorTBLRepository.Add(new ContactForProductErrorTBL
                {
                    ProductTBLId = model.ProductTBL_VM.ID,
                    Message = model.Message,
                    SenderUserTBLId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                });
                return RedirectToAction("ProductDetail", "Home", new { ProductId = model.ProductTBL_VM.ID, IncomingMessage = "Your issue has been sent successfully!" });
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ProductSupport()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var productComplains = unitOfWork.ContactForProductErrorTBLRepository.GetAllCustomized(
                 filter: a => !a.IsDeleted && a.SenderUserTBLId == UserId,
                                 includes: new Expression<Func<ContactForProductErrorTBL, object>>[]
             {
                                            //p => p.SenderUserTBL,
                                            p => p.ProductTBL,
                                            //p => p.ProductTBL.SupplierTBL,
                                            //p => p.ProductTBL.CountryTBLPlace,
                                            //p => p.ProductTBL.ManufacturerTBL,
                                            //p => p.ProductTBL.SubCategoryTBL,
                                            //p => p.ProductTBL.SubCategoryTBL.CategoryTBL
             }).ToList();
            foreach (var item in productComplains)
            {
                item.IsSeenMessageReplyToClient = true;
                unitOfWork.ContactForProductErrorTBLRepository.Update(item);
            }
            var model = Mapper.Map<List<ContactForProductErrorTBL_VM>>(productComplains.OrderByDescending(a => a.CreationDate));
            if (model == null)
                model = new List<ContactForProductErrorTBL_VM>();
            return View(model);
        }

        #endregion

        #region Helper Methods
        private OrderTBL GetUserOrder(int orderId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return unitOfWork.OrderTBLRepository.GetAllCustomized(
                  filter: a => a.IsDeleted == false && a.ID == orderId && a.IsСurrentOrder == false,
                   includes: new Expression<Func<OrderTBL, object>>[]
                      {
                                p => p.OrderByUserTBL,
                                p => p.CountryTBL,
                                p => p.ShippingServiceTBL,
                                p => p.ShippingStatusTBL,
                                //p => p.ShippingCompanyTBL

                     }).FirstOrDefault();
        }

        private OrderTBL GetCurrentUserOrder()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return unitOfWork.OrderTBLRepository.GetAllCustomized(
                  filter: a => a.IsDeleted == false && a.OrderByUserTBLId == userId && a.IsСurrentOrder == true,
                   includes: new Expression<Func<OrderTBL, object>>[]
                      {
                                p => p.OrderByUserTBL,
                                p => p.CountryTBL,
                                p => p.ShippingServiceTBL,
                                p => p.ShippingStatusTBL,

                     }).FirstOrDefault();
        }

        private List<OrderDetailTBL> GetCurrentUserOrderDetails()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return unitOfWork.OrderDetailTBLRepository.GetAllCustomized(
                filter: a => a.IsDeleted == false && a.OrderTBL.OrderByUserTBL.Id == userId && a.OrderTBL.IsСurrentOrder == false,
                includes: new Expression<Func<OrderDetailTBL, object>>[]
                {
                                        p => p.OrderTBL,
                                        p => p.ProductTBL,

                }).OrderByDescending(p => p.CreationDate).ToList();
        }

        private List<OrderDetailTBL> GetCurrentUserOrderDetails(int orderId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return unitOfWork.OrderDetailTBLRepository.GetAllCustomized(
                filter: a => a.IsDeleted == false && a.OrderTBL.OrderByUserTBL.Id == userId && a.OrderTBL.IsСurrentOrder == true && a.OrderTBLId == orderId,
                includes: new Expression<Func<OrderDetailTBL, object>>[]
                {
                                        p => p.OrderTBL,
                                        p => p.ShippingStatusTBL,
                                        p => p.ShippingCompanyCostTBL,
                                        p => p.ShippingCompanyCostTBL.ShippingCompanyTBL,
                                        p => p.ShippingCompanyCostTBL.CountryTBLSendFrom,
                                        p => p.ShippingCompanyCostTBL.CountryTBLSendTo,
                                        p => p.ProductTBL,
                                        p => p.ProductTBL.SupplierTBL,

                }).OrderByDescending(p => p.CreationDate).ToList();
        }

        private int GetTotalItemsOfOrder(int orderId, out int totalCost)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orderDetails = unitOfWork.OrderDetailTBLRepository.GetAllCustomized(
                filter: a => a.IsDeleted == false && a.OrderTBL.OrderByUserTBL.Id == userId && a.OrderTBL.IsСurrentOrder == true && a.OrderTBLId == orderId,
                includes: new Expression<Func<OrderDetailTBL, object>>[]
                {
            p => p.ProductTBL
                }).OrderByDescending(p => p.CreationDate).ToList();

            int totalProducts = Convert.ToInt32(orderDetails.Sum(od => od.Quantity));

            var subtotal = orderDetails.Sum(od => od.Quantity * od.Price);
            var totalShippingCost = orderDetails.Sum(od => od.Quantity * od.ShippingCost);
            totalCost = Convert.ToInt32(subtotal + totalShippingCost);

            return totalProducts;
        }

        private List<OrderDetailTBL> GetCompletedUserOrderDetails(int orderId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return unitOfWork.OrderDetailTBLRepository.GetAllCustomized(
                filter: a => a.IsDeleted == false && a.OrderTBL.OrderByUserTBL.Id == userId && a.OrderTBLId == orderId,
                includes: new Expression<Func<OrderDetailTBL, object>>[]
                {
                                        p => p.OrderTBL,
                                        p => p.ProductTBL,
                                        p => p.ShippingStatusTBL,
                                        p => p.ShippingCompanyCostTBL,
                                        p => p.ShippingCompanyCostTBL.ShippingCompanyTBL,
                                        p => p.ShippingCompanyCostTBL.CountryTBLSendFrom,
                                        p => p.ShippingCompanyCostTBL.CountryTBLSendTo,

                }).OrderByDescending(p => p.CreationDate).ToList();
        }

        private async Task AddNewOrderForCurrentUser()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = unitOfWork.UserManager.FindByIdAsync(userId);
            unitOfWork.OrderTBLRepository.Add(new OrderTBL() { IsСurrentOrder = true, OrderByUserTBLId = userId, HashCode = "ORD" + unitOfWork.MySPECIALGUID.GetUniqueKey(10).ToUpper(), CountryTBLId = GetCurrentUser().Result.CountryTBLId });
        }

        private async Task<bool> IfOrderCountryandUserCountry()
        {
            var CurrentOrder = GetCurrentUserOrder();
            var UserCountry = await GetCurrentUser();
            if (CurrentOrder != null && CurrentOrder.CountryTBLId != UserCountry.CountryTBLId)
            {
                CurrentOrder.IsDeleted = true;
                CurrentOrder.IsСurrentOrder = false;
                unitOfWork.OrderTBLRepository.Update(CurrentOrder);
                await AddNewOrderForCurrentUser();
                return true;
            }
            return false;
        }

        private async Task<AppUser> GetCurrentUser()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await unitOfWork.UserManager.FindByIdAsync(userId);
        }

        private ShippingCompanyCostTBL GetShippingCompanyCost(int productId, int countryTo)
        {
            var ProductFrom = unitOfWork.ProductTBLRepository.GetById(productId).CountryTBLPlaceId;
            return unitOfWork.ShippingCompanyCostTBLRepository
                 .GetAllCustomized(filter: a => a.IsDeleted == false && a.CountryTBLSendFromId == ProductFrom && a.CountryTBLSendToId == countryTo).FirstOrDefault();
        }

        private bool ISProductISAllowedToThisUser(int productId, int countryTo)
        {
            var ProductFrom = unitOfWork.ProductTBLRepository.GetById(productId).CountryTBLPlaceId;
            return unitOfWork.ShippingCompanyCostTBLRepository
                 .GetAllCustomized(filter: a => a.IsDeleted == false && a.CountryTBLSendFromId == ProductFrom && a.CountryTBLSendToId == countryTo).Any();
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
