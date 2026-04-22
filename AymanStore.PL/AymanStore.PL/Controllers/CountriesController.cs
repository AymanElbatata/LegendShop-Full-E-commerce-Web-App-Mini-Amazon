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
    public class CountriesController : Controller
    {
        private readonly ILogger<CountriesController> logger;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;
        private readonly IConfiguration configuration;

        public CountriesController(ILogger<CountriesController> logger, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            Mapper = mapper;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var data = unitOfWork.CountryTBLRepository.GetAllCustomized(
                filter: a => !a.IsDeleted).OrderBy(a => a.Name).ToList();
            return View(Mapper.Map<List<CountryTBL_VM>>(data));
        }


        [HttpPost]
        public IActionResult AddCountry(string Name, string Code)
        {
            try
            {
                bool Exist = unitOfWork.CountryTBLRepository.GetAll().Any(c => c.Name.ToLower() == Name.ToLower() && !c.IsDeleted);
                if (Exist)
                {
                    return Json(new { success = false, message = "Country already exists" });
                }

                var country = new CountryTBL
                {
                    Name = Name,
                };

                unitOfWork.CountryTBLRepository.Add(country);

                return Json(new { success = true, id = country.ID, message = "Country added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Update Country
        [HttpPost]
        public IActionResult UpdateCountry(int Id, string Name, string Code)
        {
            try
            {
                var country = unitOfWork.CountryTBLRepository.GetById(Id);
                if (country == null)
                    return Json(new { success = false, message = "Country not found" });

                bool Exist = unitOfWork.CountryTBLRepository.GetAll().Any(c => c.Name.ToLower() == Name.ToLower() && c.ID != Id && !c.IsDeleted);
                if (Exist)
                {
                    return Json(new { success = false, message = "Country already exists" });
                }

                country.Name = Name;
                country.LastUpdateDate = DateTime.Now;

                unitOfWork.CountryTBLRepository.Update(country);

                return Json(new { success = true, message = "Country updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Delete Country
        [HttpPost]
        public IActionResult DeleteCountry(int id)
        {
            try
            {
                var country =  unitOfWork.CountryTBLRepository.GetById(id);
                if (country == null)
                    return Json(new { success = false, message = "Country not found" });

                country.IsDeleted = true;
                unitOfWork.CountryTBLRepository.Update(country);

                return Json(new { success = true, message = "Country deleted successfully" });
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
