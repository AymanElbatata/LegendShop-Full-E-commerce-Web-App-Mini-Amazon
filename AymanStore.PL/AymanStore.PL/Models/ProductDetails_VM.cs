using AymanStore.DAL.Entities;

namespace AymanStore.PL.Models
{
    public class ProductDetails_VM
    {
        public ProductTBL_VM ProductTBL_VM { get; set; } = new ProductTBL_VM();
        public List<ProductPhotoTBL_VM> ProductPhotoTBL_VM { get; set; } = new List<ProductPhotoTBL_VM>();
        public List<CountryTBL_VM> CountryTBL_VM { get; set; } = new List<CountryTBL_VM>();
        public List<ProductSpecificationTBL_VM> ProductSpecificationTBL_VM { get; set; } = new List<ProductSpecificationTBL_VM>();
        public List<ProductRatingTBL_VM> ProductRatingTBL_VM { get; set; } = new List<ProductRatingTBL_VM>();
        public ShippingCompanyCostTBL_VM ShippingCompanyCostTBL_VM { get; set; } = new ShippingCompanyCostTBL_VM();
        public double ProductRate { get; set; } = 0;
    }



}
