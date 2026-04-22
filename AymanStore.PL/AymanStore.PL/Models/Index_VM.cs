using AymanStore.DAL.Entities;

namespace AymanStore.PL.Models
{
    public class Index_VM
    {
        public List<SubCategoryTBL_VM> SubCategoryTBL_VM { get; set; } = new List<SubCategoryTBL_VM>();
        public List<ProductTBL_VM> ProductTBL_VM { get; set; } = new List<ProductTBL_VM>();
        public List<CategoryTBL_VM> CategoryTBL_VM { get; set; } = new List<CategoryTBL_VM>();
        public List<ProductRatingTBL_VM> ProductRatingTBL_VM { get; set; } = new List<ProductRatingTBL_VM>();
    }



}
