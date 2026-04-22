using AutoMapper;
using AymanStore.BLL.Repositories;
using AymanStore.DAL.Entities;
using AymanStore.PL.Models;
using Microsoft.AspNetCore.Identity;

namespace AymanStore.PL.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CategoryTBL, CategoryTBL_VM>().ReverseMap();
            CreateMap<SubCategoryTBL, SubCategoryTBL_VM>().ReverseMap();
            CreateMap<CountryTBL, CountryTBL_VM>().ReverseMap();
            CreateMap<GenderTBL, GenderTBL_VM>().ReverseMap();
            CreateMap<ManufacturerTBL, ManufacturerTBL_VM>().ReverseMap();
            CreateMap<ProductTBL, ProductTBL_VM>().ReverseMap();
            CreateMap<ProductPhotoTBL, ProductPhotoTBL_VM>().ReverseMap();
            CreateMap<ProductRatingTBL, ProductRatingTBL_VM>().ReverseMap();
            CreateMap<ProductSpecificationTBL, ProductSpecificationTBL_VM>().ReverseMap();
            CreateMap<ShippingStatusTBL, ShippingStatusTBL_VM>().ReverseMap();
            CreateMap<ShippingCompanyTBL, ShippingCompanyTBL_VM>().ReverseMap();
            CreateMap<ShippingCompanyCostTBL, ShippingCompanyCostTBL_VM>().ReverseMap();
            CreateMap<ShippingServiceTBL, ShippingServiceTBL_VM>().ReverseMap();
            CreateMap<OrderTBL, OrderTBL_VM>().ReverseMap();
            CreateMap<OrderDetailTBL, OrderDetailTBL_VM>().ReverseMap();
            CreateMap<InvoiceOrderDetailTBL, InvoiceOrderDetailTBL_VM>().ReverseMap();
            CreateMap<ProductSpecificationTBL, ProductSpecificationTBL_VM>().ReverseMap();
            CreateMap<ProductPhotoTBL, ProductPhotoTBL_VM>().ReverseMap();
            CreateMap<EmailTBL, EmailTBL_VM>().ReverseMap();
            CreateMap<ContactUsTBL, ContactUsTBL_VM>().ReverseMap();
            CreateMap<ContactForProductErrorTBL, ContactForProductErrorTBL_VM>().ReverseMap();
            CreateMap<AbuseProductRatingTBL, AbuseProductRatingTBL_VM>().ReverseMap();
            CreateMap<InvoiceRateTBL, InvoiceRateTBL_VM>().ReverseMap();
        }
    }
}
