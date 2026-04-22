using AutoMapper;
using AymanStore.BLL.Interfaces;
using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Entities;
using AymanStore.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AymanStore.PL.Controllers
{
    [Authorize(Roles = "Supplier, Admin")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> logger;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;
        private readonly IConfiguration configuration;

        public ProductController(ILogger<ProductController> logger, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            Mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new Products_VM();
            var user = await GetCurrentUser();

            var products = await GetProductsByUserId(user.Id);
            model.ProductTBL_VM = Mapper.Map<List<ProductTBL_VM>>(products.OrderByDescending(p => p.CreationDate));
            var productIds = model.ProductTBL_VM.Select(p => p.ID).ToList();

            // Get ratings ONLY for those specific products
            model.ProductPhotoTBL_VM = Mapper.Map<List<ProductPhotoTBL_VM>>(
                unitOfWork.ProductPhotoTBLRepository.GetAllCustomized(
                    filter: a => a.IsDeleted == false && productIds.Contains(Convert.ToInt32(a.ProductTBLId))
                ).OrderByDescending(a => a.CreationDate).ToList());

            // Get ratings ONLY for those specific products
            model.ProductSpecificationTBL_VM = Mapper.Map<List<ProductSpecificationTBL_VM>>(
                unitOfWork.ProductSpecificationTBLRepository.GetAllCustomized(
                    filter: a => a.IsDeleted == false && productIds.Contains(Convert.ToInt32(a.ProductTBLId))
                ).OrderByDescending(a => a.CreationDate).ToList());
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> IndexAdmin()
        {
            var model = new Products_VM();

            var products = await GetProductsForAdmin();
            model.ProductTBL_VM = Mapper.Map<List<ProductTBL_VM>>(products.OrderByDescending(p => p.CreationDate));
            var productIds = model.ProductTBL_VM.Select(p => p.ID).ToList();

            // Get ratings ONLY for those specific products
            model.ProductPhotoTBL_VM = Mapper.Map<List<ProductPhotoTBL_VM>>(
                unitOfWork.ProductPhotoTBLRepository.GetAllCustomized(
                    filter: a => a.IsDeleted == false && productIds.Contains(Convert.ToInt32(a.ProductTBLId))
                ).OrderByDescending(a => a.CreationDate).ToList());

            // Get ratings ONLY for those specific products
            model.ProductSpecificationTBL_VM = Mapper.Map<List<ProductSpecificationTBL_VM>>(
                unitOfWork.ProductSpecificationTBLRepository.GetAllCustomized(
                    filter: a => a.IsDeleted == false && productIds.Contains(Convert.ToInt32(a.ProductTBLId))
                ).OrderByDescending(a => a.CreationDate).ToList());
            return View(model);
        }

        #region ComplainProducts & Create Product

        [HttpGet]
        public IActionResult ComplainProducts()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var productComplains = unitOfWork.ContactForProductErrorTBLRepository.GetAllCustomized(
                 filter: a => !a.IsDeleted && a.ProductTBL.SupplierTBLId == UserId,
                                 includes: new Expression<Func<ContactForProductErrorTBL, object>>[]
             {
                                            //p => p.SenderUserTBL,
                                            p => p.ProductTBL,
                                            //p => p.ProductTBL.SupplierTBL,
                                            //p => p.ProductTBL.CountryTBLPlace,
                                            //p => p.ProductTBL.ManufacturerTBL,
                                            //p => p.ProductTBL.SubCategoryTBL,
                                            //p => p.ProductTBL.SubCategoryTBL.CategoryTBL
             }).ToList();
            var model = Mapper.Map<List<ContactForProductErrorTBL_VM>>(productComplains.OrderByDescending(a=>a.CreationDate));
            if (model == null)
                model = new List<ContactForProductErrorTBL_VM>();
            return View(model);
        }

        [HttpPost]
        public IActionResult ReplyToComplaint(int complaintId, string replyMessage)
        {
            var complaint = unitOfWork.ContactForProductErrorTBLRepository
                .GetById(complaintId);

            if (complaint != null)
            { 
            complaint.IsSeenMessageReplyToClient = false;
            complaint.MessageReplyToClient = replyMessage;
            complaint.LastUpdateDate = DateTime.Now;
            unitOfWork.ContactForProductErrorTBLRepository.Update(complaint);
            }
            return RedirectToAction("ComplainProducts","Product");
        }


        [HttpGet]
        public IActionResult Create()
        {
            string Barcode = "PRD" + unitOfWork.MySPECIALGUID.GetUniqueKey(10).ToUpper();
            var newProduct = unitOfWork.ProductTBLRepository.Add(new ProductTBL
            {
                Name = "New Product",
                SupplierTBLId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Barcode = Barcode,
                SubCategoryTBLId = 1
            });
            return RedirectToAction("Index", "Product");
        }
        #endregion

        #region SearchProducts & GetProductForEdit

        [HttpGet]
        public async Task<IActionResult> SearchProducts(string searchTerm)
        {
            try
            {
                var user = await GetCurrentUser();
                var products = await GetProductsByUserId(user.Id);

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(p =>
                        p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        (p.Brand != null && p.Brand.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                        (p.Barcode != null && p.Barcode.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                        (p.Model != null && p.Model.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
                }

                var model = new Products_VM();
                model.ProductTBL_VM = Mapper.Map<List<ProductTBL_VM>>(products.OrderByDescending(p => p.CreationDate));
                var productIds = model.ProductTBL_VM.Select(p => p.ID).ToList();

                // Get ratings ONLY for those specific products
                model.ProductPhotoTBL_VM = Mapper.Map<List<ProductPhotoTBL_VM>>(
                    unitOfWork.ProductPhotoTBLRepository.GetAllCustomized(
                        filter: a => a.IsDeleted == false && productIds.Contains(Convert.ToInt32(a.ProductTBLId))
                    ).OrderByDescending(a => a.CreationDate).ToList());

                // Get ratings ONLY for those specific products
                model.ProductSpecificationTBL_VM = Mapper.Map<List<ProductSpecificationTBL_VM>>(
                    unitOfWork.ProductSpecificationTBLRepository.GetAllCustomized(
                        filter: a => a.IsDeleted == false && productIds.Contains(Convert.ToInt32(a.ProductTBLId))
                    ).OrderByDescending(a => a.CreationDate).ToList());
                return PartialView("_ProductsListPartial", model);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetProductForEdit(int id)
        {
            var product = unitOfWork.ProductTBLRepository.GetAllCustomized(
                filter: a => !a.IsDeleted && a.ID == id,
                                includes: new Expression<Func<ProductTBL, object>>[]
            {
                                p => p.CountryTBLPlace,
                                p => p.ManufacturerTBL,
                                p => p.SubCategoryTBL,
                                p => p.SubCategoryTBL.CategoryTBL

            }).FirstOrDefault();
            var productVM = Mapper.Map<ProductTBL_VM>(product);
            return PartialView("_EditProductPartial", productVM);
        }
        #endregion

        #region Update Product & Delete Product

        // Update Product
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(string productJson, IFormFile? MainPhoto)
        {
            try
            {
                var model = JsonSerializer.Deserialize<ProductTBL_VM>(productJson);

                var product = unitOfWork.ProductTBLRepository.GetById(model.ID);
                if (product == null)
                    return Json(new { success = false, message = "Product not found" });

                // Handle main photo upload
                if (MainPhoto != null && MainPhoto.Length > 0)
                {
                    // Delete old photo if exists
                    if (!string.IsNullOrEmpty(product.Image))
                    {
                        var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Products", product.Image);
                        if (System.IO.File.Exists(oldPhotoPath))
                            System.IO.File.Delete(oldPhotoPath);
                    }

                    // Upload new photo
                    var fileName = $"{product.Barcode}_{Guid.NewGuid()}_{MainPhoto.FileName}";
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Products");

                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    var filePath = Path.Combine(uploadPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await MainPhoto.CopyToAsync(stream);
                    }

                    product.Image = fileName;
                }

                // Update other properties
                product.Name = model.Name;
                product.Description = model.Description;
                product.SellingPrice = model.SellingPrice;
                product.PurchasingPrice = model.PurchasingPrice;
                product.Quantity = model.Quantity;
                product.Brand = model.Brand;
                product.Model = model.Model;
                product.Material = model.Material;
                product.Color = model.Color;
                product.Weight = model.Weight;
                product.Size = model.Size;
                //product.Barcode = model.Barcode;
                product.IsPublished = model.IsPublished;
                product.ValidFrom = model.ValidFrom;
                product.SubCategoryTBLId = model.SubCategoryTBLId;
                product.ManufacturerTBLId = model.ManufacturerTBLId;
                product.CountryTBLPlaceId = model.CountryTBLPlaceId;
                product.LastUpdateDate = DateTime.Now;

                unitOfWork.ProductTBLRepository.Update(product);

                return Json(new { success = true, message = "Product updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        // Delete Product
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var product = unitOfWork.ProductTBLRepository.GetById(id);
                if (product == null)
                    return Json(new { success = false, message = "Product not found" });
                if (IsProductRelatedtoAnyOrder(id).Result)
                    return Json(new { success = false, message = "Product is Related to Orders" });
                // Soft delete
                product.IsDeleted = true;
                unitOfWork.ProductTBLRepository.Update(product);

                return Json(new { success = true, message = "Product deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region UploadProductPhoto & DeleteProductPhoto & AddProductSpecification & DeleteProductSpecification
        // Upload Product Photo
        [HttpPost]
        public async Task<IActionResult> UploadProductPhoto(int productId, IFormFile photo)
        {
            try
            {
                if (photo == null || photo.Length == 0)
                    return Json(new { success = false, message = "No photo selected" });

                var product = unitOfWork.ProductTBLRepository.GetById(productId);

                // Generate unique filename
                var fileName = $"{product.Barcode}_{Guid.NewGuid()}_{photo.FileName}";
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Products");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                var photoUrl = fileName;

                var productPhoto = new ProductPhotoTBL
                {
                    ProductTBLId = productId,
                    Image = photoUrl,
                };

                unitOfWork.ProductPhotoTBLRepository.Add(productPhoto);

                return Json(new { success = true, message = "Photo uploaded successfully", photoId = productPhoto.ID, imageName = productPhoto.Image });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Delete Product Photo
        [HttpPost]
        public IActionResult DeleteProductPhoto(int id)
        {
            try
            {
                var photo = unitOfWork.ProductPhotoTBLRepository.GetById(id);
                if (photo == null)
                    return Json(new { success = false, message = "Photo not found" });

                // Delete physical file
                //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Products", photo.Image);
                //if (System.IO.File.Exists(filePath))
                //    System.IO.File.Delete(filePath);

                // Soft delete
                photo.IsDeleted = true;
                unitOfWork.ProductPhotoTBLRepository.Update(photo);

                return Json(new { success = true, message = "Photo deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Add Product Specification
        [HttpPost]
        public IActionResult AddProductSpecification(int productId, string value)
        {
            try
            {
                var specification = new ProductSpecificationTBL
                {
                    ProductTBLId = productId,
                    Name = value,
                };

                unitOfWork.ProductSpecificationTBLRepository.Add(specification);

                return Json(new { success = true, message = "Specification added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Delete Product Specification
        [HttpPost]
        public async Task<IActionResult> DeleteProductSpecification(int id)
        {
            try
            {
                var spec = unitOfWork.ProductSpecificationTBLRepository.GetById(id);
                if (spec == null)
                    return Json(new { success = false, message = "Specification not found" });

                spec.IsDeleted = true;
                unitOfWork.ProductSpecificationTBLRepository.Update(spec);

                return Json(new { success = true, message = "Specification deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region Get Categories & Get SubCategories & Get Manufacturers & Get Countries
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = unitOfWork.CategoryTBLRepository.GetAllCustomized(
                filter: sc => sc.IsDeleted == false);
            return Json(categories.Select(c => new { id = c.ID, name = c.Name }));
        }

        // Get SubCategories by Category
        [HttpGet]
        public async Task<IActionResult> GetSubCategories(int categoryId)
        {
            var subCategories = unitOfWork.SubCategoryTBLRepository.GetAllCustomized(
                filter: sc => sc.CategoryTBLId == categoryId && sc.IsDeleted == false
            );
            return Json(subCategories.Select(sc => new { id = sc.ID, name = sc.Name }));
        }

        // Get single SubCategory
        [HttpGet]
        public async Task<IActionResult> GetSubCategory(int id)
        {
            var subCategory = unitOfWork.SubCategoryTBLRepository.GetById(id);
            return Json(new { id = subCategory.ID, name = subCategory.Name, categoryId = subCategory.CategoryTBLId });
        }

        // Get Manufacturers
        [HttpGet]
        public async Task<IActionResult> GetManufacturers()
        {
            var manufacturers = unitOfWork.ManufacturerTBLRepository.GetAllCustomized(
                filter: sc => sc.IsDeleted == false);
            return Json(manufacturers.Select(m => new { id = m.ID, name = m.Name }));
        }

        // Get Countries
        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var countries = unitOfWork.CountryTBLRepository.GetAllCustomized(
                filter: sc => sc.IsDeleted == false);
            return Json(countries.Select(c => new { id = c.ID, name = c.Name }));
        }

        #endregion

        #region Helper Methods
        private async Task<AppUser> GetCurrentUser()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await unitOfWork.UserManager.FindByIdAsync(userId);
        }

        private async Task<List<ProductTBL>> GetProductsByUserId(string userId)
        {
            var products = unitOfWork.ProductTBLRepository.GetAllCustomized(
                filter: a => !a.IsDeleted && a.SupplierTBLId == userId,
                                includes: new Expression<Func<ProductTBL, object>>[]
            {
                                p => p.CountryTBLPlace,
                                p => p.ManufacturerTBL,
                                p => p.SubCategoryTBL,
                                p => p.SubCategoryTBL.CategoryTBL

            }).ToList();
            return products;
        }

        private async Task<List<ProductTBL>> GetProductsForAdmin()
        {
            var products = unitOfWork.ProductTBLRepository.GetAllCustomized(
                filter: a => !a.IsDeleted,
                                includes: new Expression<Func<ProductTBL, object>>[]
            {
                                p => p.CountryTBLPlace,
                                p => p.ManufacturerTBL,
                                p => p.SubCategoryTBL,
                                p => p.SubCategoryTBL.CategoryTBL

            }).ToList();
            return products;
        }

        private async Task<bool> IsProductRelatedtoAnyOrder(int producId)
        {
            var isRelated = unitOfWork.OrderDetailTBLRepository.GetAllCustomized(
                filter: od => !od.IsDeleted && od.ProductTBLId == Convert.ToInt32(producId)
            ).Any();
            return isRelated;
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
