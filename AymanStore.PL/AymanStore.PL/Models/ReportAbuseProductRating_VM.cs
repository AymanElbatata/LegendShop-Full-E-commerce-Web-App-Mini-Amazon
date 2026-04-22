namespace AymanStore.PL.Models
{
    public class ReportAbuseProductRating_VM
    {
        public ProductRatingTBL_VM ProductRatingTBL_VM { get; set; } = new ProductRatingTBL_VM();
        public AbuseProductRatingTBL_VM AbuseProductRatingTBL_VM { get; set; } = new AbuseProductRatingTBL_VM();
    }
}
