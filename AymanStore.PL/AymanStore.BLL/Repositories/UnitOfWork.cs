using AymanStore.BLL.Interfaces;
using AymanStore.BLL.IRepositories;
using AymanStore.DAL.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace AymanStore.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenderTBLRepository GenderTBLRepository { get; }
        public ICountryTBLRepository CountryTBLRepository { get; }
        public IProductTBLRepository ProductTBLRepository { get; }
        public IProductPhotoTBLRepository ProductPhotoTBLRepository { get; }
        public IProductRatingTBLRepository ProductRatingTBLRepository { get; }
        public IProductSpecificationTBLRepository ProductSpecificationTBLRepository { get; }
        public ICategoryTBLRepository CategoryTBLRepository { get; }
        public ISubCategoryTBLRepository SubCategoryTBLRepository { get; }
        public IEmailTBLRepository EmailTBLRepository { get; }
        public IShippingStatusTBLRepository ShippingStatusTBLRepository { get; }
        public IShippingCompanyTBLRepository ShippingCompanyTBLRepository { get; }
        public IShippingCompanyCostTBLRepository ShippingCompanyCostTBLRepository { get; }
        public IShippingServiceTBLRepository ShippingServiceTBLRepository { get; }
        public IOrderTBLRepository OrderTBLRepository { get; }
        public IOrderDetailTBLRepository OrderDetailTBLRepository { get; }
        public IInvoiceOrderDetailTBLRepository InvoiceOrderDetailTBLRepository { get; }
        public IManufacturerTBLRepository ManufacturerTBLRepository { get; }
        public IAppErrorTBLRepository AppErrorTBLRepository { get; }
        public SignInManager<AppUser> SignInManager { get; }
        public RoleManager<AppRole> RoleManager { get; }
        public UserManager<AppUser> UserManager { get; }
        public IMySPECIALGUID MySPECIALGUID { get; }
        public IContactForProductErrorTBLRepository ContactForProductErrorTBLRepository { get; }
        public IAbuseProductRatingTBLRepository AbuseProductRatingTBLRepository { get; }
        public IInvoiceRateTBLRepository InvoiceRateTBLRepository { get; }
        public IContactUsRepository ContactUsRepository { get; }
        public IGeoLocationHelper GeoLocationHelper { get; }


        public UnitOfWork( IGenderTBLRepository GenderTBLRepository
            ,ICountryTBLRepository CountryTBLRepository,
            IProductTBLRepository ProductTBLRepository,
            IProductPhotoTBLRepository ProductPhotoTBLRepository,
            IProductRatingTBLRepository ProductRatingTBLRepository,
            IProductSpecificationTBLRepository ProductSpecificationTBLRepository,
            ICategoryTBLRepository CategoryTBLRepository,
            ISubCategoryTBLRepository SubCategoryTBLRepository,
            IEmailTBLRepository EmailTBLRepository,
            IShippingStatusTBLRepository ShippingStatusTBLRepository,
            IShippingCompanyTBLRepository ShippingCompanyTBLRepository,
            IShippingCompanyCostTBLRepository ShippingCompanyCostTBLRepository,
            IShippingServiceTBLRepository ShippingServiceTBLRepository,
            IOrderTBLRepository OrderTBLRepository,
            IOrderDetailTBLRepository OrderDetailTBLRepository,
            IInvoiceOrderDetailTBLRepository InvoiceOrderDetailTBLRepository,
            IManufacturerTBLRepository ManufacturerTBLRepository,
            IAppErrorTBLRepository AppErrorTBLRepository,

            SignInManager<AppUser> SignInManager,
            RoleManager<AppRole> RoleManager, UserManager<AppUser> UserManager,
            IMySPECIALGUID MySPECIALGUID, IContactForProductErrorTBLRepository ContactForProductErrorTBLRepository,
            IContactUsRepository ContactUsRepository, IAbuseProductRatingTBLRepository AbuseProductRatingTBLRepository,
            IInvoiceRateTBLRepository InvoiceRateTBLRepository,
            IGeoLocationHelper GeoLocationHelper
            )
        {
            this.CountryTBLRepository = CountryTBLRepository;
            this.GenderTBLRepository = GenderTBLRepository;
            this.ProductTBLRepository = ProductTBLRepository;
            this.ProductPhotoTBLRepository = ProductPhotoTBLRepository;
            this.ProductRatingTBLRepository = ProductRatingTBLRepository;
            this.ProductSpecificationTBLRepository = ProductSpecificationTBLRepository;
            this.CategoryTBLRepository = CategoryTBLRepository;
            this.SubCategoryTBLRepository = SubCategoryTBLRepository;
            this.EmailTBLRepository = EmailTBLRepository;
            this.ShippingStatusTBLRepository = ShippingStatusTBLRepository;
            this.ShippingCompanyTBLRepository = ShippingCompanyTBLRepository;
            this.ShippingCompanyCostTBLRepository = ShippingCompanyCostTBLRepository;
            this.ShippingServiceTBLRepository = ShippingServiceTBLRepository;
            this.OrderTBLRepository = OrderTBLRepository;
            this.OrderDetailTBLRepository = OrderDetailTBLRepository;
            this.InvoiceOrderDetailTBLRepository = InvoiceOrderDetailTBLRepository;
            this.ManufacturerTBLRepository = ManufacturerTBLRepository;
            this.AppErrorTBLRepository = AppErrorTBLRepository;
            this.SignInManager = SignInManager;
            this.RoleManager = RoleManager;
            this.UserManager = UserManager;
            this.MySPECIALGUID = MySPECIALGUID;
            this.ContactForProductErrorTBLRepository = ContactForProductErrorTBLRepository;
            this.AbuseProductRatingTBLRepository = AbuseProductRatingTBLRepository;
            this.InvoiceRateTBLRepository = InvoiceRateTBLRepository;
            this.ContactUsRepository = ContactUsRepository;
            this.GeoLocationHelper = GeoLocationHelper;
        }
    }
}
