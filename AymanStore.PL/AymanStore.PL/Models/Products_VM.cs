using AymanStore.DAL.Entities;

namespace AymanStore.PL.Models
{
    public class Products_VM
    {
        public List<ProductTBL_VM> ProductTBL_VM { get; set; } = new List<ProductTBL_VM>();
        public List<ProductPhotoTBL_VM> ProductPhotoTBL_VM { get; set; } = new List<ProductPhotoTBL_VM>();
        public List<ProductSpecificationTBL_VM> ProductSpecificationTBL_VM { get; set; } = new List<ProductSpecificationTBL_VM>();

    }
}
