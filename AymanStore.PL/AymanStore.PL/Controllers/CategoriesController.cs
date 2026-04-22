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
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> logger;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;
        private readonly IConfiguration configuration;

        public CategoriesController(ILogger<CategoriesController> logger, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            Mapper = mapper;
            this.logger = logger;
        }
        public IActionResult Index()
        {
            var data = unitOfWork.CategoryTBLRepository.GetAllCustomized(
                filter: a => !a.IsDeleted).OrderByDescending(a=>a.CreationDate).ToList();
            return View(Mapper.Map<List<CategoryTBL_VM>>(data));
        }

        [HttpPost]
        public IActionResult AddCategory(string Name, string Description)
        {
            try
            {
                bool Exist = unitOfWork.CategoryTBLRepository.GetAll().Any(c => c.Name.ToLower() == Name.ToLower() && !c.IsDeleted);
                if (Exist)
                    return Json(new { success = false, message = "Category name already exists" });
                var category = new CategoryTBL
                {
                    Name = Name,
                    Description = Description,
                };

                 unitOfWork.CategoryTBLRepository.Add(category);

                return Json(new { success = true, id = category.ID, message = "Category added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdateCategory(int Id, string Name, string Description)
        {
            try
            {
                var category = unitOfWork.CategoryTBLRepository.GetById(Id);
                if (category == null)
                    return Json(new { success = false, message = "Category not found" });

                bool Exist = unitOfWork.CategoryTBLRepository.GetAll().Any(c => c.Name.ToLower() == Name.ToLower() && c.ID != Id && !c.IsDeleted);
                if (Exist)
                    return Json(new { success = false, message = "Category name already exists" });

                category.Name = Name;
                category.Description = Description;
                category.LastUpdateDate = DateTime.Now;

                unitOfWork.CategoryTBLRepository.Update(category);

                return Json(new { success = true, message = "Category updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var category = unitOfWork.CategoryTBLRepository.GetById(id);
                if (category == null)
                    return Json(new { success = false, message = "Category not found" });

                var subCategories = unitOfWork.SubCategoryTBLRepository.GetAll().Where(s => s.CategoryTBLId == id).ToList();
                foreach (var sub in subCategories)
                {
                    sub.IsDeleted = true;
                    unitOfWork.SubCategoryTBLRepository.Update(sub);
                }
                category.IsDeleted = true;
                unitOfWork.CategoryTBLRepository.Update(category);

                return Json(new { success = true, message = "Category deleted successfully" });
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
