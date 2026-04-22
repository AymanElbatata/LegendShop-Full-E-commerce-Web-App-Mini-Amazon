using AymanStore.BLL.Interfaces;
using AymanStore.BLL.IRepositories;
using AymanStore.BLL.Repositories;
using AymanStore.DAL.BaseEntity;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;
using AymanStore.PL.Controllers;
using AymanStore.PL.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewOptions(options =>
    {
        options.HtmlHelperOptions.ClientValidationEnabled = true;
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1); // Session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<AymanStoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed(_ => true));
});


//services.AddSingleton 

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMySPECIALGUID, MySPECIALGUID>();
builder.Services.AddScoped<ICountryTBLRepository, CountryTBLRepository>();
builder.Services.AddScoped<IGenderTBLRepository, GenderTBLRepository>();
builder.Services.AddScoped<IProductTBLRepository, ProductTBLRepository>();
builder.Services.AddScoped<IProductPhotoTBLRepository, ProductPhotoTBLRepository>();
builder.Services.AddScoped<IProductRatingTBLRepository, ProductRatingTBLRepository>();
builder.Services.AddScoped<IProductSpecificationTBLRepository, ProductSpecificationTBLRepository>();
builder.Services.AddScoped<ICategoryTBLRepository, CategoryTBLRepository>();
builder.Services.AddScoped<ISubCategoryTBLRepository, SubCategoryTBLRepository>();
builder.Services.AddScoped<IShippingStatusTBLRepository, ShippingStatusTBLRepository>();
builder.Services.AddScoped<IShippingCompanyTBLRepository, ShippingCompanyTBLRepository>();
builder.Services.AddScoped<IShippingCompanyCostTBLRepository, ShippingCompanyCostTBLRepository>();
builder.Services.AddScoped<IShippingServiceTBLRepository, ShippingServiceTBLRepository>();
builder.Services.AddScoped<IOrderTBLRepository, OrderTBLRepository>();
builder.Services.AddScoped<IOrderDetailTBLRepository, OrderDetailTBLRepository>();
builder.Services.AddScoped<IManufacturerTBLRepository, ManufacturerTBLRepository>();
builder.Services.AddScoped<IInvoiceOrderDetailTBLRepository, InvoiceOrderDetailTBLRepository>();
builder.Services.AddScoped<IAppErrorTBLRepository, AppErrorTBLRepository>();
builder.Services.AddScoped<IEmailTBLRepository, EmailTBLRepository>();
builder.Services.AddScoped<IContactForProductErrorTBLRepository, ContactForProductErrorTBLRepository>();
builder.Services.AddScoped<IAbuseProductRatingTBLRepository, AbuseProductRatingTBLRepository>();
builder.Services.AddScoped<IInvoiceRateTBLRepository, InvoiceRateTBLRepository>();
builder.Services.AddScoped<IContactUsRepository, ContactUsRepository>();

builder.Services.AddScoped<IGeoLocationHelper, GeoLocationHelper>();
//services.AddTransient


builder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfiles()));



builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<AymanStoreDbContext>()
.AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Home/Error";

    options.ExpireTimeSpan = TimeSpan.FromDays(1); // ÌáÓÉ 24 ÓÇÚÉ
    options.SlidingExpiration = true;

    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = ".LegendSoft.AntiForgery";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.Zero;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
SeedInitialData.SeedData(app);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");
app.UseSession();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionHandlerPathFeature?.Error != null)
        {
            var ex = exceptionHandlerPathFeature.Error;

            // Log
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Global exception caught at {Path}", exceptionHandlerPathFeature.Path);

            // Save to DB
            using var scope = context.RequestServices.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<IAppErrorTBLRepository>();
            repo.Add(new AppErrorTBL
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace ?? "",
                Controller = exceptionHandlerPathFeature.Path ?? "",
                Action = "" // optional
            });
        }

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
        {
            error = "An unexpected error occurred. Please try again later."
        }));
    });
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
