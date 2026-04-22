using AutoMapper;
using AymanStore.BLL.Interfaces;
using AymanStore.BLL.Repositories;
using AymanStore.DAL.Entities;
using AymanStore.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace AymanStore.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SubCategoriesController : Controller
    {
        private readonly ILogger<SubCategoriesController> logger;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;
        private readonly IConfiguration configuration;

        public SubCategoriesController(ILogger<SubCategoriesController> logger, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            Mapper = mapper;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var data = unitOfWork.SubCategoryTBLRepository.GetAllCustomized(
                 filter: a => !a.IsDeleted, includes: new Expression<Func<SubCategoryTBL, object>>[]
                {
                        p => p.CategoryTBL
                }).OrderByDescending(a => a.CreationDate).ToList();
            var categories = unitOfWork.CategoryTBLRepository.GetAllCustomized(
                        filter: c => c.IsDeleted == false).OrderByDescending(a => a.CreationDate).ToList();
            ViewBag.Categories = categories;

            return View(Mapper.Map<List<SubCategoryTBL_VM>>(data));
        }

        // POST: Add Subcategory
        [HttpPost]
        public IActionResult AddSubcategory(int CategoryTBLId, string Name, string Description)
        {
            try
            {
                bool Exist = unitOfWork.SubCategoryTBLRepository.GetAllCustomized(
                    filter: s => s.Name.ToLower() == Name.ToLower() && s.CategoryTBLId == CategoryTBLId && !s.IsDeleted).Any();
                if (Exist)
                    return Json(new { success = false, message = "SubCategory name already exists" });

                var subcategory = new SubCategoryTBL
                {
                    CategoryTBLId = CategoryTBLId,
                    Name = Name,
                    Description = Description,
                };

                 unitOfWork.SubCategoryTBLRepository.Add(subcategory);

                return Json(new { success = true, id = subcategory.ID, categoryId = CategoryTBLId, message = "Subcategory added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Update Subcategory
        [HttpPost]
        public IActionResult UpdateSubcategory(int Id, int CategoryTBLId, string Name, string Description)
        {
            try
            {
                var subcategory = unitOfWork.SubCategoryTBLRepository.GetById(Id);
                if (subcategory == null)
                    return Json(new { success = false, message = "Subcategory not found" });

                bool Exist = unitOfWork.SubCategoryTBLRepository.GetAllCustomized(
                    filter: s => s.Name.ToLower() == Name.ToLower() && s.CategoryTBLId == CategoryTBLId && s.ID != Id && !s.IsDeleted).Any();
                if (Exist)
                    return Json(new { success = false, message = "SubCategory name already exists" });

                subcategory.CategoryTBLId = CategoryTBLId;
                subcategory.Name = Name;
                subcategory.Description = Description;
                subcategory.LastUpdateDate= DateTime.Now;

                unitOfWork.SubCategoryTBLRepository.Update(subcategory);

                return Json(new { success = true, message = "Subcategory updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Delete Subcategory
        [HttpPost]
        public IActionResult DeleteSubcategory(int id)
        {
            try
            {
                var subcategory = unitOfWork.SubCategoryTBLRepository.GetById(id);
                if (subcategory == null)
                    return Json(new { success = false, message = "Subcategory not found" });

                subcategory.IsDeleted = true;
                unitOfWork.SubCategoryTBLRepository.Update(subcategory);

                return Json(new { success = true, message = "Subcategory deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
