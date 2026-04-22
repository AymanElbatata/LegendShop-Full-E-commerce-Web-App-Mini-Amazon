using AutoMapper;
using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Entities;
using AymanStore.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq.Expressions;

namespace AymanStore.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShippingCompanyCostController : Controller
    {
        private readonly ILogger<ShippingCompanyCostController> logger;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;
        private readonly IConfiguration configuration;

        public ShippingCompanyCostController(ILogger<ShippingCompanyCostController> logger, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            Mapper = mapper;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var data = unitOfWork.ShippingCompanyCostTBLRepository.GetAllCustomized(
                filter: a => !a.IsDeleted, includes: new Expression<Func<ShippingCompanyCostTBL, object>>[]
                {
                        p => p.ShippingCompanyTBL,
                        p => p.CountryTBLSendFrom,
                        p => p.CountryTBLSendTo
                }).OrderByDescending(a => a.CreationDate).ToList();

            var shippingCompanies = unitOfWork.ShippingCompanyTBLRepository.GetAllCustomized(
            filter: c => c.IsDeleted == false).OrderBy(a => a.Name).ToList();

            var countries = unitOfWork.CountryTBLRepository.GetAllCustomized(
                filter: c => c.IsDeleted == false).OrderBy(a => a.Name).ToList();

            ViewBag.ShippingCompanies = shippingCompanies;
            ViewBag.Countries = countries;

            return View(Mapper.Map<List<ShippingCompanyCostTBL_VM>>(data));
        }


        [HttpPost]
        public IActionResult AddShippingCost(int ShippingCompanyTBLId, int CountryTBLSendFromId, int CountryTBLSendToId, int Cost)
        {
            try
            {
                var shippingCost = new ShippingCompanyCostTBL
                {
                    ShippingCompanyTBLId = ShippingCompanyTBLId,
                    CountryTBLSendFromId = CountryTBLSendFromId,
                    CountryTBLSendToId = CountryTBLSendToId,
                    Cost = Cost,
                };
                bool Exist =  unitOfWork.ShippingCompanyCostTBLRepository.GetAllCustomized().Any(s => s.CountryTBLSendFromId == CountryTBLSendFromId && s.CountryTBLSendToId == CountryTBLSendToId && !s.IsDeleted);
                if (Exist)
                    return Json(new { success = false, message = "Shipping cost already exists for this combination" });

                unitOfWork.ShippingCompanyCostTBLRepository.Add(shippingCost);

                return Json(new
                {
                    success = true,
                    id = shippingCost.ID,
                    shippingCompanyId = ShippingCompanyTBLId,
                    sendFromId = CountryTBLSendFromId,
                    sendToId = CountryTBLSendToId,
                    message = "Shipping cost added successfully"
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Update Shipping Cost
        [HttpPost]
        public IActionResult UpdateShippingCost(int Id, int ShippingCompanyTBLId, int CountryTBLSendFromId, int CountryTBLSendToId, int Cost)
        {
            try
            {
                var shippingCost =  unitOfWork.ShippingCompanyCostTBLRepository.GetById(Id);
                if (shippingCost == null)
                    return Json(new { success = false, message = "Shipping cost not found" });

                bool Exist =  unitOfWork.ShippingCompanyCostTBLRepository.GetAllCustomized().Any(s => s.ID != Id && s.CountryTBLSendFromId == CountryTBLSendFromId && s.CountryTBLSendToId == CountryTBLSendToId && !s.IsDeleted);
                if (Exist)
                    return Json(new { success = false, message = "Shipping cost already exists for this combination" });
    
                shippingCost.ShippingCompanyTBLId = ShippingCompanyTBLId;
                shippingCost.CountryTBLSendFromId = CountryTBLSendFromId;
                shippingCost.CountryTBLSendToId = CountryTBLSendToId;
                shippingCost.Cost = Cost;

                unitOfWork.ShippingCompanyCostTBLRepository.Update(shippingCost);

                return Json(new { success = true, message = "Shipping cost updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Delete Shipping Cost
        [HttpPost]
        public IActionResult DeleteShippingCost(int id)
        {
            try
            {
                var shippingCost =  unitOfWork.ShippingCompanyCostTBLRepository.GetById(id);
                if (shippingCost == null)
                    return Json(new { success = false, message = "Shipping cost not found" });

                shippingCost.IsDeleted = true;
                unitOfWork.ShippingCompanyCostTBLRepository.Update(shippingCost);

                return Json(new { success = true, message = "Shipping cost deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
