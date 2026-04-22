using AutoMapper;
using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Entities;
using AymanStore.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AymanStore.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShippingServicesController : Controller
    {
        private readonly ILogger<ShippingServicesController> logger;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;
        private readonly IConfiguration configuration;

        public ShippingServicesController(ILogger<ShippingServicesController> logger, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            Mapper = mapper;
            this.logger = logger;
        }
        public IActionResult Index()
        {
            var data = unitOfWork.ShippingServiceTBLRepository.GetAllCustomized(
              filter: a => !a.IsDeleted).OrderByDescending(a => a.CreationDate).ToList();
            return View(Mapper.Map<List<ShippingServiceTBL_VM>>(data));

        }

        [HttpPost]
        public IActionResult AddShippingService(string Name, string Description)
        {
            try
            {
                var shippingService = new ShippingServiceTBL
                {
                    Name = Name,
                    Description = Description,
                };
                bool isNameExist = unitOfWork.ShippingServiceTBLRepository.GetAllCustomized(
                    filter: s => s.Name.ToLower() == Name.ToLower() && !s.IsDeleted).Any();
                if (isNameExist)
                {
                    return Json(new { success = false, message = "Shipping service with the same name already exists" });
                }
                unitOfWork.ShippingServiceTBLRepository.Add(shippingService);

                return Json(new { success = true, id = shippingService.ID, message = "Shipping service added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Update Shipping Service
        [HttpPost]
        public IActionResult UpdateShippingService(int Id, string Name, string Description)
        {
            try
            {
                var shippingService = unitOfWork.ShippingServiceTBLRepository.GetById(Id);
                if (shippingService == null)
                    return Json(new { success = false, message = "Shipping service not found" });

                bool Exist = unitOfWork.ShippingServiceTBLRepository.GetAllCustomized(
                    filter: s => s.Name.ToLower() == Name.ToLower() && s.ID != Id && !s.IsDeleted).Any();
                if (Exist)
                    return Json(new { success = false, message = "Shipping service name already exists" });

                shippingService.Name = Name;
                shippingService.Description = Description;

                unitOfWork.ShippingServiceTBLRepository.Update(shippingService);

                return Json(new { success = true, message = "Shipping service updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Delete Shipping Service
        [HttpPost]
        public IActionResult DeleteShippingService(int id)
        {
            try
            {
                var shippingService =  unitOfWork.ShippingServiceTBLRepository.GetById(id);
                if (shippingService == null)
                    return Json(new { success = false, message = "Shipping service not found" });

                shippingService.IsDeleted = true;
                unitOfWork.ShippingServiceTBLRepository.Update(shippingService);

                return Json(new { success = true, message = "Shipping service deleted successfully" });
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
