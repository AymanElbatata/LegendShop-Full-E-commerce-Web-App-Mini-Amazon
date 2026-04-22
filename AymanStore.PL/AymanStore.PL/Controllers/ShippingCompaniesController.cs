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
    public class ShippingCompaniesController : Controller
    {
        private readonly ILogger<ShippingCompaniesController> logger;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;
        private readonly IConfiguration configuration;

        public ShippingCompaniesController(ILogger<ShippingCompaniesController> logger, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            Mapper = mapper;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var data = unitOfWork.ShippingCompanyTBLRepository.GetAllCustomized(
                filter: a => !a.IsDeleted).OrderByDescending(a => a.CreationDate).ToList();
            return View(Mapper.Map<List<ShippingCompanyTBL_VM>>(data));
        }

        [HttpPost]
        public IActionResult AddCompany(string Name, string Description)
        {
            try
            {
                bool Exist = unitOfWork.ShippingCompanyTBLRepository.GetAll().Any(c => c.Name.ToLower() == Name.ToLower() && !c.IsDeleted);
                if (Exist)
                    return Json(new { success = false, message = "Company name already exists" });
                var company = new ShippingCompanyTBL
                {
                    Name = Name,
                    Description = Description,
                };

                unitOfWork.ShippingCompanyTBLRepository.Add(company);

                return Json(new { success = true, id = company.ID, message = "Category added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdateCompany(int Id, string Name, string Description)
        {
            try
            {
                var company = unitOfWork.ShippingCompanyTBLRepository.GetById(Id);
                if (company == null)
                    return Json(new { success = false, message = "Company not found" });

                bool Exist = unitOfWork.ShippingCompanyTBLRepository.GetAll().Any(c => c.Name.ToLower() == Name.ToLower() && c.ID != Id && !c.IsDeleted);
                if (Exist)
                    return Json(new { success = false, message = "Company name already exists" });

                company.Name = Name;
                company.Description = Description;
                company.LastUpdateDate = DateTime.Now;

                unitOfWork.ShippingCompanyTBLRepository.Update(company);

                return Json(new { success = true, message = "Company updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DeleteCompany(int id)
        {
            try
            {
                var company = unitOfWork.ShippingCompanyTBLRepository.GetById(id);
                if (company == null)
                    return Json(new { success = false, message = "Company not found" });

                company.IsDeleted = true;
                unitOfWork.ShippingCompanyTBLRepository.Update(company);

                return Json(new { success = true, message = "Company deleted successfully" });
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
