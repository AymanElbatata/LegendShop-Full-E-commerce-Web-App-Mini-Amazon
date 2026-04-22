using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;
//using Exam.DAL.Interfaces;
//using Exam.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AYMDating.DAL.Contexts
{
    public class AymanStoreDbContextSeed
    {
        public static async Task SeedAsync(AymanStoreDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,  ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.CountryTBLs.Any())
                {
                    var Countries = File.ReadAllText("../AymanStore.DAL/Contexts/SeedData/Country.json");
                    var CountryCollection = JsonSerializer.Deserialize<List<CountryTBL>>(Countries);
                    for (int i = 0; i < CountryCollection?.Count; i++)
                    {
                        context.CountryTBLs.Add(CountryCollection[i]);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.GenderTBLs.Any())
                {
                    var Genders = File.ReadAllText("../AymanStore.DAL/Contexts/SeedData/Gender.json");
                    var GenderCollection = JsonSerializer.Deserialize<List<GenderTBL>>(Genders);
                    for (int i = 0; i < GenderCollection?.Count; i++)
                    {
                        context.GenderTBLs.Add(GenderCollection[i]);
                    }
                    await context.SaveChangesAsync();
                }

                if (!roleManager.Roles.Any())
                {
                    var role1 = new AppRole
                    {
                        Name = "Admin"
                    };
                    var role2 = new AppRole
                    {
                        Name = "User"
                    };

                    var role3 = new AppRole
                    {
                        Name = "Supplier"
                    };

                    await roleManager.CreateAsync(role1);
                    await roleManager.CreateAsync(role2);
                    await roleManager.CreateAsync(role3);
                }

                if (!userManager.Users.Any())
                {
                    var user1 = new AppUser
                    {
                        NormalizedUserName = "AymanElbatata".ToUpper(),
                        Email = "Ayman.Fathy.Elbatata@gmail.com",
                        UserName = "Ayman.Elbatata",
                        FirstName = "Ayman",
                        LastName = "Elbatata",
                        IsActivated = true,
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        PhoneNumber = "201284878483",
                        CountryTBLId = 50,
                        GenderTBLId = 1,
                        Address = "Egypt, Cairo, Nasr City",
                    };
                    var user2 = new AppUser
                    {
                        NormalizedUserName = "AymanFathy".ToUpper(),
                        Email = "Ayman_Elbatata@outlook.com",
                        UserName = "Ayman.Fathy",
                        FirstName = "Ayman",
                        LastName = "Fathy",
                        IsActivated = true,
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        PhoneNumber = "201554820255",
                        CountryTBLId = 50,
                        GenderTBLId = 1,
                        Address = "Egypt, Cairo, Tanta City",
                    };

                    var user3 = new AppUser
                    {
                        NormalizedUserName = "AymanBatata".ToUpper(),
                        Email = "Ayman_Elbatata@inbox.lt",
                        UserName = "Ayman.Batata",
                        FirstName = "Ayman",
                        LastName = "Fathy",
                        IsActivated = true,
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        PhoneNumber = "201202025251",
                        CountryTBLId = 50,
                        GenderTBLId = 1,
                        Address = "Egypt, Red Sea, Hurghada",
                    };

                    await userManager.CreateAsync(user1, "Aym@8511");
                    await userManager.CreateAsync(user2, "Aym@8511");
                    await userManager.CreateAsync(user3, "Aym@8511");

                    await userManager.AddToRoleAsync(user1, "Admin");
                    await userManager.AddToRoleAsync(user2, "User");
                    await userManager.AddToRoleAsync(user3, "Supplier");
                    await context.SaveChangesAsync();


                    if (!context.CategoryTBLs.Any())
                    {
                        var Categories = File.ReadAllText("../AymanStore.DAL/Contexts/SeedData/Category.json");
                        var CategoryCollection = JsonSerializer.Deserialize<List<CategoryTBL>>(Categories);
                        for (int i = 0; i < CategoryCollection?.Count; i++)
                        {
                            context.CategoryTBLs.Add(CategoryCollection[i]);
                        }

                        await context.SaveChangesAsync();
                    }
                    if (!context.SubCategoryTBLs.Any())
                    {
                        var SubCategories = File.ReadAllText("../AymanStore.DAL/Contexts/SeedData/SubCategory.json");
                        var SubCategoryCollection = JsonSerializer.Deserialize<List<SubCategoryTBL>>(SubCategories);
                        for (int i = 0; i < SubCategoryCollection?.Count; i++)
                        {
                            context.SubCategoryTBLs.Add(SubCategoryCollection[i]);
                        }

                        await context.SaveChangesAsync();

                    }

                    if (!context.ManufacturerTBLs.Any())
                    {
                        var Manufactureres = File.ReadAllText("../AymanStore.DAL/Contexts/SeedData/Manufacturer.json");
                        var ManufacturerCollection = JsonSerializer.Deserialize<List<ManufacturerTBL>>(Manufactureres);
                        for (int i = 0; i < ManufacturerCollection?.Count; i++)
                        {
                            context.ManufacturerTBLs.Add(ManufacturerCollection[i]);
                        }
                        await context.SaveChangesAsync();
                    }

                    if (!context.ShippingServiceTBLs.Any())
                    {
                        var ShippingService = File.ReadAllText("../AymanStore.DAL/Contexts/SeedData/ShippingService.json");
                        var ShippingServiceCollection = JsonSerializer.Deserialize<List<ShippingServiceTBL>>(ShippingService);
                        for (int i = 0; i < ShippingServiceCollection?.Count; i++)
                        {
                            context.ShippingServiceTBLs.Add(ShippingServiceCollection[i]);
                        }
                        await context.SaveChangesAsync();
                    }
                    if (!context.ProductTBLs.Any())
                    {
                        var user = await userManager.FindByEmailAsync("Ayman_Elbatata@outlook.com");

                        var Products = File.ReadAllText("../AymanStore.DAL/Contexts/SeedData/Product.json");
                        var ProductCollection = JsonSerializer.Deserialize<List<ProductTBL>>(Products);
                        for (int i = 0; i < ProductCollection?.Count; i++)
                        {
                            ProductCollection[i].SupplierTBLId = user3.Id;
                            context.ProductTBLs.Add(ProductCollection[i]);
                        }
                        await context.SaveChangesAsync();
                    }
                    if (!context.ProductPhotoTBLs.Any())
                    {
                        var ProductPhotos = File.ReadAllText("../AymanStore.DAL/Contexts/SeedData/ProductPhoto.json");
                        var ProductPhotoCollection = JsonSerializer.Deserialize<List<ProductPhotoTBL>>(ProductPhotos);
                        for (int i = 0; i < ProductPhotoCollection?.Count; i++)
                        {
                            context.ProductPhotoTBLs.Add(ProductPhotoCollection[i]);
                        }
                        await context.SaveChangesAsync();
                    }

                    if (!context.ShippingStatusTBLs.Any())
                    {
                        var ShippingStatus = File.ReadAllText("../AymanStore.DAL/Contexts/SeedData/ShippingStatus.json");
                        var ShippingStatusCollection = JsonSerializer.Deserialize<List<ShippingStatusTBL>>(ShippingStatus);
                        for (int i = 0; i < ShippingStatusCollection?.Count; i++)
                        {
                            context.ShippingStatusTBLs.Add(ShippingStatusCollection[i]);
                        }
                        await context.SaveChangesAsync();
                    }

                    if (!context.ShippingCompanyTBLs.Any())
                    {
                        var ShippingCompany = File.ReadAllText("../AymanStore.DAL/Contexts/SeedData/ShippingCompany.json");
                        var ShippingCompanyCollection = JsonSerializer.Deserialize<List<ShippingCompanyTBL>>(ShippingCompany);
                        for (int i = 0; i < ShippingCompanyCollection?.Count; i++)
                        {
                            context.ShippingCompanyTBLs.Add(ShippingCompanyCollection[i]);
                        }
                        await context.SaveChangesAsync();
                    }

                    if (!context.ShippingCompanyCostTBLs.Any())
                    {
                        var ShippingCompanyCost = File.ReadAllText("../AymanStore.DAL/Contexts/SeedData/ShippingCompanyCost.json");
                        var ShippingCompanyCostCollection = JsonSerializer.Deserialize<List<ShippingCompanyCostTBL>>(ShippingCompanyCost);
                        for (int i = 0; i < ShippingCompanyCostCollection?.Count; i++)
                        {
                            context.ShippingCompanyCostTBLs.Add(ShippingCompanyCostCollection[i]);
                        }
                        await context.SaveChangesAsync();
                    }
                    if (!context.InvoiceRateTBLs.Any())
                    {
                        var InvoiceRates = File.ReadAllText("../AymanStore.DAL/Contexts/SeedData/InvoiceRate.json");
                        var InvoiceRatesCollection = JsonSerializer.Deserialize<List<InvoiceRateTBL>>(InvoiceRates);
                        for (int i = 0; i < InvoiceRatesCollection?.Count; i++)
                        {
                            context.InvoiceRateTBLs.Add(InvoiceRatesCollection[i]);
                        }
                        await context.SaveChangesAsync();
                    }

                    if (!context.ProductRatingTBLs.Any())
                    {
                        var user = await userManager.FindByEmailAsync("Ayman_Elbatata@outlook.com");
                        for (int i = 1; i < 6; i++)
                        {
                            context.ProductRatingTBLs.Add(new ProductRatingTBL { AppUserWhoRatedId = user.Id, Stars = i, ProductTBLId = 4, ReviewSubject = "Good Product: " + i, ReviewComment = "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." + i, IsHelpful = (i == 2 || i == 4) ? true : false });
                        }
                        await context.SaveChangesAsync();
                    }

                    if (!context.ProductSpecificationTBLs.Any())
                    {
                        for (int i = 1; i < 6; i++)
                        {
                            context.ProductSpecificationTBLs.Add(new ProductSpecificationTBL { ProductTBLId = 4, Name = "is simply dummy text of the printing: " + i });
                        }
                        await context.SaveChangesAsync();
                    }

                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<AymanStoreDbContext>();
                logger.LogError(ex.Message);
            }
        }



    }
}
