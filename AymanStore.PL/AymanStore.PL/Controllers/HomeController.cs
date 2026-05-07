using AspNetCoreGeneratedDocument;
using AutoMapper;
using AymanStore.BLL.Interfaces;
using AymanStore.BLL.Repositories;
using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Entities;
using AymanStore.PL.Models;
using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AymanStore.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IMapper Mapper)
        {
            _logger = logger;
            this.unitOfWork = unitOfWork;
            this.Mapper = Mapper;

        }

        #region Index & Search Products in Home Page

        [HttpGet]
        public IActionResult Index()
        {
            return View(GetAllRequiredProducts(null,null));
        }

        [HttpPost]
        public IActionResult Search(string? MainSearchWord, int? CategorySearchId)
        {
            ViewBag.MainSearchWord = MainSearchWord;
            ViewBag.CategorySearchId = CategorySearchId;
            return View(GetAllRequiredProducts(MainSearchWord, CategorySearchId));
        }
        #endregion

        #region Product Detail
        public IActionResult ProductDetail(int? ProductId, string? IncomingMessage)
        {
            if (ProductId == 0 || !unitOfWork.ProductTBLRepository.GetAllCustomized(
                filter: a => !a.IsDeleted && a.IsPublished && a.ID == ProductId).Any())
                return RedirectToAction("Index", "Home");

            if (!string.IsNullOrEmpty(IncomingMessage))
            {
                ViewBag.Message = IncomingMessage;
            }

            var data = new ProductDetails_VM();
            var Product = unitOfWork.ProductTBLRepository.GetAllCustomized(
            filter: a => a.IsDeleted == false && a.ID == ProductId,
            includes: new Expression<Func<ProductTBL, object>>[]
            {
                                                        p => p.SubCategoryTBL.CategoryTBL,
                                                        p => p.SubCategoryTBL,
                                                        p => p.ManufacturerTBL,
                                                        p => p.SupplierTBL,
                                                        p => p.CountryTBLPlace

            }).FirstOrDefault();
            if (Product != null)
            {
                data.ProductTBL_VM = Mapper.Map<ProductTBL_VM>(Product);

                var productPhotos = unitOfWork.ProductPhotoTBLRepository.GetAllCustomized(
                         filter: a => a.IsDeleted == false && a.ProductTBLId == ProductId);
                data.ProductPhotoTBL_VM = Mapper.Map<List<ProductPhotoTBL_VM>>(productPhotos.OrderByDescending(p => p.CreationDate));
            }

            var fromCountryId = unitOfWork.CountryTBLRepository.GetAllCustomized(
               filter: a => a.IsDeleted == false && a.ID == Product.CountryTBLPlaceId).FirstOrDefault().ID;

            string IncomingcountryName = string.Empty;
            int MycountryId = 0;
            string UserIdLogged = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(UserIdLogged))
            {
                MycountryId = (int)unitOfWork.UserManager.FindByIdAsync(UserIdLogged).Result.CountryTBLId;
                IncomingcountryName = unitOfWork.CountryTBLRepository.GetById(MycountryId).Name;
            }
            else if (string.IsNullOrEmpty(IncomingcountryName))
                IncomingcountryName = HttpContext.Session.GetString("currentUserCountry");

            if (!string.IsNullOrEmpty(IncomingcountryName))
                MycountryId = unitOfWork.CountryTBLRepository.GetAllCustomized(
                      filter: a => a.IsDeleted == false && a.Name.Contains(IncomingcountryName)).FirstOrDefault().ID;


            var shippingCompanyCost_shippingCountries = unitOfWork.ShippingCompanyCostTBLRepository.GetAllCustomized(
              filter: a => a.IsDeleted == false
              && a.CountryTBLSendFromId == fromCountryId,
              includes: new Expression<Func<ShippingCompanyCostTBL, object>>[]
              {
                                                                           p => p.ShippingCompanyTBL,
                                                                           p => p.CountryTBLSendTo
              }).OrderBy(p => p.CountryTBLSendTo.Name);

            var Countries = new List<CountryTBL>();
            Countries.AddRange(shippingCompanyCost_shippingCountries.Select(a => a.CountryTBLSendTo).OrderBy(a => a.Name));

            data.CountryTBL_VM = Mapper.Map<List<CountryTBL_VM>>(Countries);


            if (MycountryId > 0)
            {
                data.ShippingCompanyCostTBL_VM = Mapper.Map<ShippingCompanyCostTBL_VM>(shippingCompanyCost_shippingCountries.Where(a => a.CountryTBLSendToId == MycountryId).FirstOrDefault());
                if (data.ShippingCompanyCostTBL_VM == null)
                {
                    //data.ShippingCompanyCostTBL_VM = Mapper.Map<ShippingCompanyCostTBL_VM>(shippingCompanyCost_shippingCountries.FirstOrDefault());
                    data.ShippingCompanyCostTBL_VM = new ShippingCompanyCostTBL_VM();
                }
            }

            var ProductRatingList = unitOfWork.ProductRatingTBLRepository.GetAllCustomized(
                   filter: a => a.IsDeleted == false && a.ProductTBLId == Product.ID,
             includes: new Expression<Func<ProductRatingTBL, object>>[]
             {
                                                        p => p.AppUserWhoRated,
             }); ;
            data.ProductRatingTBL_VM = Mapper.Map<List<ProductRatingTBL_VM>>(ProductRatingList.OrderByDescending(p => p.CreationDate));
            data.ProductRate = GetAverageRatingAsync(Product.ID);
            data.ProductSpecificationTBL_VM = Mapper.Map<List<ProductSpecificationTBL_VM>>(unitOfWork.ProductSpecificationTBLRepository.GetAllCustomized(
                         filter: a => a.IsDeleted == false && a.ProductTBLId == ProductId).OrderByDescending(p => p.CreationDate));

            return View(data);
        }
        #endregion

        #region Save Current User Country JSon Result
        [HttpPost]
        public IActionResult SaveCurrentUserCountry(string? currentUserCountry)
        {
            // Example: save to DB or log it
            if (currentUserCountry != null)
            {
                HttpContext.Session.SetString("currentUserCountry", currentUserCountry);
            }
            return Json(new { success = true });
        }
        #endregion

        #region Search Products By Category/SubCategory
        public IActionResult ProductsByCategoryId(int? CategoryId)
        {
            if (CategoryId > 0)
            {
                var data = new Index_VM();

                var Products = unitOfWork.ProductTBLRepository.GetAllCustomized(
                    filter: a => a.IsDeleted == false && a.SubCategoryTBL.CategoryTBLId == CategoryId,
                    includes: new Expression<Func<ProductTBL, object>>[]
                    {
                                             p => p.SubCategoryTBL.CategoryTBL,
                                             p => p.SubCategoryTBL,
                                             p => p.ManufacturerTBL
                    });
                if (Products.Count() > 0)
                {
                    data.ProductTBL_VM = Mapper.Map<List<ProductTBL_VM>>(Products.OrderByDescending(p => p.CreationDate));
                    foreach (var item in data.ProductTBL_VM)
                    {
                        data.ProductRatingTBL_VM.AddRange(Mapper.Map<List<ProductRatingTBL_VM>>(unitOfWork.ProductRatingTBLRepository.GetAllCustomized(
                        filter: a => a.IsDeleted == false && a.ProductTBLId == item.ID).ToList()));
                    }
                    return View(data);
                }
            }
            ViewBag.CategoryName = unitOfWork.CategoryTBLRepository.GetAllCustomized(
                        filter: a => a.IsDeleted == false && a.ID == CategoryId).FirstOrDefault().Name ?? "NA";
            return View(new Index_VM());
        }

        public IActionResult ProductsBySubCategoryId(int? SubCategoryId)
        {
            if (SubCategoryId > 0)
            {
                var data = new Index_VM();

                var Products = unitOfWork.ProductTBLRepository.GetAllCustomized(
                    filter: a => a.IsDeleted == false && a.SubCategoryTBL.ID == SubCategoryId,
                    includes: new Expression<Func<ProductTBL, object>>[]
                    {
                                             p => p.SubCategoryTBL.CategoryTBL,
                                             p => p.SubCategoryTBL,
                                             p => p.ManufacturerTBL
                    });
                if (Products.Count() > 0)
                {
                    data.ProductTBL_VM = Mapper.Map<List<ProductTBL_VM>>(Products.OrderByDescending(p => p.CreationDate));
                    foreach (var item in data.ProductTBL_VM)
                    {
                        data.ProductRatingTBL_VM.AddRange(Mapper.Map<List<ProductRatingTBL_VM>>(unitOfWork.ProductRatingTBLRepository.GetAllCustomized(
                        filter: a => a.IsDeleted == false && a.ProductTBLId == item.ID).ToList()));
                    }
                    return View(data);
                }
            }

            var SubCategory = unitOfWork.SubCategoryTBLRepository.GetAllCustomized(
                        filter: a => a.IsDeleted == false && a.ID == SubCategoryId,
                        includes: ue => ue.CategoryTBL).FirstOrDefault();
            if (SubCategory != null)
            {
                ViewBag.SubCategoryName = SubCategory.Name;
                ViewBag.CategoryName = SubCategory.CategoryTBL.Name;
            }

            return View(new Index_VM());
        }
        #endregion

        #region Tracking Order
        [HttpGet]
        public IActionResult TrackingOrder(string? trackingCode)
        {
            if (string.IsNullOrEmpty(trackingCode))
                return View(new OrderDetailTBL_VM());

            var RequiredOrder = unitOfWork.OrderDetailTBLRepository.GetAllCustomized(
              filter: a => a.IsDeleted == false && a.OrderTBL.TrackingShippingCode == trackingCode && a.OrderTBL.IsСurrentOrder == false,
               includes: new Expression<Func<OrderDetailTBL, object>>[]
                  {
                                        p => p.OrderTBL.CountryTBL,
                                        p => p.OrderTBL.ShippingServiceTBL,
                                        p => p.OrderTBL.ShippingStatusTBL,
                                        p => p.ShippingCompanyCostTBL,
                                        p => p.ShippingCompanyCostTBL.ShippingCompanyTBL,

                 }).FirstOrDefault();

            if (RequiredOrder == null)
                return View(new OrderDetailTBL_VM());

            return View(Mapper.Map<OrderDetailTBL_VM>(RequiredOrder));
        }

        [HttpPost]
        public IActionResult TrackingOrder(string? trackingCode, int type = 0)
        {
            return RedirectToAction("TrackingOrder", new { trackingCode = trackingCode });
        }
        #endregion

        #region General ContactUs
        [HttpGet]
        public IActionResult ContactUs()
        {
            var model = new ContactUsTBL_VM();
            string UserIdLogged = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(UserIdLogged))
            {
                var User = unitOfWork.UserManager.FindByIdAsync(UserIdLogged).Result;
                model.Email = User.Email;
                model.Name = User.FirstName + " " + User.LastName;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ContactUs(ContactUsTBL_VM model)
        {
            if (ModelState.IsValid)
            {
                var contactUs = Mapper.Map<ContactUsTBL>(model);
                unitOfWork.ContactUsRepository.Add(contactUs);

                TempData["SuccessMessage"] = "Your message has been sent successfully";

                return RedirectToAction(nameof(ContactUs));
            }

            return View(model);
        }
        #endregion

        #region Main Views
        public IActionResult Products()
        {
            return View();
        }
        public IActionResult Categories()
        {
            return View();
        }
        public IActionResult ConditionsofUse()
        {
            return View();
        }

        public IActionResult PrivacyNotice()
        {
            return View();
        }

        public IActionResult PrivacyPolicy()
        {
            return View();
        }
        public IActionResult ReturnPolicy()
        {
            return View();
        }

        public IActionResult HelpPage()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult Shipping_Returns()
        {
            return View();
        }
        public IActionResult Size_Guide()
        {
            return View();
        }

        public IActionResult ShippingDetails(int countryId)
        {
            var model = new ShippingCompanyCostTBL_VM();
            model = Mapper.Map<ShippingCompanyCostTBL_VM>(unitOfWork.ShippingCompanyCostTBLRepository.GetAllCustomized(
            filter: a => a.IsDeleted == false && a.CountryTBLSendToId == countryId,
            includes: new Expression<Func<ShippingCompanyCostTBL, object>>[]
            {
                        p => p.ShippingCompanyTBL,
                        p => p.CountryTBLSendFrom,
                        p => p.CountryTBLSendTo
            }).FirstOrDefault());
            return View(model);
        }
        #endregion

        #region Helper Methods
        private double GetAverageRatingAsync(int productId)
        {
            var ratings = unitOfWork.ProductRatingTBLRepository.GetAllCustomized(
                       filter: a => a.IsDeleted == false && a.ProductTBLId == productId);
            if (!ratings.Any())
                return 0.0; // No ratings yet

            return Math.Round(ratings.Average(r => r.Stars), 1); // Example: 4.2

        }

        private Index_VM GetAllRequiredProducts(string? MainSearchWord, int? CategorySearchId)
        {
            var data = new Index_VM();

            if (!string.IsNullOrEmpty(MainSearchWord) || CategorySearchId > 0)
            {
                var products = unitOfWork.ProductTBLRepository.GetAllCustomized(
                    filter: a => !a.IsDeleted && a.IsPublished,
                    includes: new Expression<Func<ProductTBL, object>>[]
                    {
                        p => p.SubCategoryTBL,
                        p => p.SubCategoryTBL.CategoryTBL
                    })
                    .Where(u =>
                        string.IsNullOrWhiteSpace(MainSearchWord) ||
                        u.Name.Contains(MainSearchWord) ||
                        u.Description.Contains(MainSearchWord) ||
                        u.Barcode.Contains(MainSearchWord))
                    .Where(u =>
                        !CategorySearchId.HasValue || CategorySearchId == 0 ||
                        u.SubCategoryTBL.CategoryTBL.ID == CategorySearchId.Value)
                    .OrderByDescending(p => p.CreationDate)
                    .ToList();

                data.ProductTBL_VM = Mapper.Map<List<ProductTBL_VM>>(products);
            }
            else
            {
                var Product = unitOfWork.ProductTBLRepository.GetAllCustomized(
                    filter: a => !a.IsDeleted && a.IsPublished,
                    includes: new Expression<Func<ProductTBL, object>>[]
                    {
                                    p => p.SubCategoryTBL,
                                    p => p.SubCategoryTBL.CategoryTBL
                    })
                    .OrderByDescending(a => a.CreationDate)
                    .Take(12)
                    .ToList();

                data.ProductTBL_VM = Mapper.Map<List<ProductTBL_VM>>(Product);
            }
            if (data.ProductTBL_VM.Count() > 0)
            {
                // Get the product IDs from the selected products
                var productIds = data.ProductTBL_VM.Select(p => p.ID).ToList();

                // Get ratings ONLY for those specific products
                data.ProductRatingTBL_VM = Mapper.Map<List<ProductRatingTBL_VM>>(
                    unitOfWork.ProductRatingTBLRepository.GetAllCustomized(
                        filter: a => a.IsDeleted == false && productIds.Contains(Convert.ToInt32(a.ProductTBLId))
                    )
                );
            }
            return data;
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
