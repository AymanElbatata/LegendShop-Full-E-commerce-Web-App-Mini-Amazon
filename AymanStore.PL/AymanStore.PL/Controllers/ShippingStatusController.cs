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
    public class ShippingStatusController : Controller
    {
        private readonly ILogger<ShippingStatusController> logger;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;
        private readonly IConfiguration configuration;

        public ShippingStatusController(ILogger<ShippingStatusController> logger, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            Mapper = mapper;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var data = unitOfWork.ShippingStatusTBLRepository.GetAllCustomized(
                filter: a => !a.IsDeleted).OrderByDescending(a => a.CreationDate).ToList();
            return View(Mapper.Map<List<ShippingStatusTBL_VM>>(data));
        }


        [HttpPost]
        public IActionResult AddStatus(string Name, string Description)
        {
            try
            {
                bool Exist = unitOfWork.ShippingStatusTBLRepository.GetAll().Any(c => c.Name.ToLower() == Name.ToLower() && !c.IsDeleted);
                if (Exist)
                    return Json(new { success = false, message = "Status name already exists" });
                var status = new ShippingStatusTBL
                {
                    Name = Name,
                    Description = Description,
                };

                unitOfWork.ShippingStatusTBLRepository.Add(status);

                return Json(new { success = true, id = status.ID, message = "Status added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdateStatus(int Id, string Name, string Description)
        {
            try
            {
                var status = unitOfWork.ShippingStatusTBLRepository.GetById(Id);
                if (status == null)
                    return Json(new { success = false, message = "Status not found" });

                bool Exist = unitOfWork.ShippingStatusTBLRepository.GetAll().Any(c => c.Name.ToLower() == Name.ToLower() && c.ID != Id && !c.IsDeleted);
                if (Exist)
                    return Json(new { success = false, message = "Status name already exists" });

                status.Name = Name;
                status.Description = Description;
                status.LastUpdateDate = DateTime.Now;

                unitOfWork.ShippingStatusTBLRepository.Update(status);

                return Json(new { success = true, message = "Status updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DeleteStatus(int id)
        {
            try
            {
                var Status = unitOfWork.ShippingStatusTBLRepository.GetById(id);
                if (Status == null)
                    return Json(new { success = false, message = "Status not found" });

                if (Status.ID > 5)
                {
                    Status.IsDeleted = true;
                    unitOfWork.ShippingStatusTBLRepository.Update(Status);
                    return Json(new { success = true, message = "Status deleted successfully" });
                }

                return Json(new { success = false, message = "Cannot delete this Status" });
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
