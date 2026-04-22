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
    public class InvoiceRatesController : Controller
    {
        private readonly ILogger<InvoiceRatesController> logger;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;
        private readonly IConfiguration configuration;

        public InvoiceRatesController(ILogger<InvoiceRatesController> logger, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            Mapper = mapper;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var data = unitOfWork.InvoiceRateTBLRepository.GetAllCustomized(
                filter: a => !a.IsDeleted).OrderByDescending(a => a.CreationDate).ToList();
            return View(Mapper.Map<List<InvoiceRateTBL_VM>>(data));
        }


        [HttpPost]
        public async Task<IActionResult> AddInvoiceRates(int VATRate, int ProfitRate)
        {
            try
            {
                // Validate rates
                if (VATRate < 0 || VATRate > 100)
                    return Json(new { success = false, message = "VAT rate must be between 0 and 100" });

                if (ProfitRate < 0 || ProfitRate > 100)
                    return Json(new { success = false, message = "Profit rate must be between 0 and 100" });

                bool exists = unitOfWork.InvoiceRateTBLRepository.GetAllCustomized(
                filter: a => !a.IsDeleted).Any();
                if (exists)
                    return Json(new { success = false, message = "Rates already exist" });

                var rates = new InvoiceRateTBL
                {
                    VATRate = VATRate,
                    ProfitRate = ProfitRate,
                };

                 unitOfWork.InvoiceRateTBLRepository.Add(rates);

                return Json(new { success = true, id = rates.ID, message = "Rates added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Update Invoice Rates
        [HttpPost]
        public async Task<IActionResult> UpdateInvoiceRates(int ID, int VATRate, int ProfitRate)
        {
            try
            {
                // Validate rates
                if (VATRate < 0 || VATRate > 100)
                    return Json(new { success = false, message = "VAT rate must be between 0 and 100" });

                if (ProfitRate < 0 || ProfitRate > 100)
                    return Json(new { success = false, message = "Profit rate must be between 0 and 100" });

                var rates =  unitOfWork.InvoiceRateTBLRepository.GetById(ID);
                if (rates == null)
                    return Json(new { success = false, message = "Rates not found" });

                rates.VATRate = VATRate;
                rates.ProfitRate = ProfitRate;
                rates.LastUpdateDate = DateTime.Now;

                unitOfWork.InvoiceRateTBLRepository.Update(rates);

                return Json(new { success = true, message = "Rates updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Delete Invoice Rates
        [HttpPost]
        public async Task<IActionResult> DeleteInvoiceRates(int id)
        {
            try
            {
                var rates =  unitOfWork.InvoiceRateTBLRepository.GetById(id);
                if (rates == null)
                    return Json(new { success = false, message = "Rates not found" });

                rates.IsDeleted = true;
                unitOfWork.InvoiceRateTBLRepository.Update(rates);

                return Json(new { success = true, message = "Rates deleted successfully" });
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
