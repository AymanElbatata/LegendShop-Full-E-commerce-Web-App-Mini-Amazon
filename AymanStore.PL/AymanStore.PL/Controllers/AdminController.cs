using AutoMapper;
using AymanStore.BLL.Interfaces;
using AymanStore.BLL.Repositories;
using AymanStore.DAL.Entities;
using AymanStore.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AymanStore.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> logger;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;
        private readonly IConfiguration configuration;

        public AdminController(ILogger<AdminController> logger, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            Mapper = mapper;
            this.logger = logger;
        }
        public IActionResult Index()
        {
            var totalUsers = unitOfWork.UserManager.Users.Where(u => u.EmailConfirmed).ToList();
            ViewBag.TotalSuppliers = totalUsers.Where(u => unitOfWork.UserManager.IsInRoleAsync(u, "Supplier").Result)
                .Count();
            ViewBag.TotalUsers = totalUsers.Where(u => unitOfWork.UserManager.IsInRoleAsync(u, "User").Result)
                .Count();
            ViewBag.TotalProducts = unitOfWork.ProductTBLRepository.GetAllCustomized(filter: o => !o.IsDeleted).Count();
            ViewBag.TotalOrders = unitOfWork.OrderTBLRepository.GetAllCustomized(filter: c => !c.IsDeleted && c.DateOfSubmit != null).Count();

            return View();
        }

        #region ProductComplaints & ContactUsMessages & ReviewsReports
        [HttpGet]
        public IActionResult ProductComplaints()
        {
            var productComplains = unitOfWork.ContactForProductErrorTBLRepository.GetAllCustomized(
                 filter: a => !a.IsDeleted,
                                 includes: new Expression<Func<ContactForProductErrorTBL, object>>[]
             {
                                            p => p.SenderUserTBL,
                                            p => p.ProductTBL,
                                            p => p.ProductTBL.SupplierTBL,
             }).ToList();
            var model = Mapper.Map<List<ContactForProductErrorTBL_VM>>(productComplains.OrderByDescending(a => a.CreationDate));
            if (model == null)
                model = new List<ContactForProductErrorTBL_VM>();
            return View(model);
        }


        [HttpGet]
        public IActionResult ContactUsMessages()
        {
            var contactUsMsg = unitOfWork.ContactUsRepository.GetAllCustomized(
                 filter: a => !a.IsDeleted).ToList();
            var model = Mapper.Map<List<ContactUsTBL_VM>>(contactUsMsg.OrderByDescending(a => a.CreationDate));
            if (model == null)
                model = new List<ContactUsTBL_VM>();
            return View(model);
        }

        [HttpGet]
        public IActionResult ReviewsReports()
        {
            var contactUsMsg = unitOfWork.AbuseProductRatingTBLRepository.GetAllCustomized(
                 filter: a => !a.IsDeleted,
                                 includes: new Expression<Func<AbuseProductRatingTBL, object>>[]
             {
                                            p => p.SenderUserTBL,
                                            p => p.ProductRatingTBL.ProductTBL,
             }).ToList();
            var model = Mapper.Map<List<AbuseProductRatingTBL_VM>>(contactUsMsg.OrderByDescending(a => a.CreationDate));
            if (model == null)
                model = new List<AbuseProductRatingTBL_VM>();
            return View(model);
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
