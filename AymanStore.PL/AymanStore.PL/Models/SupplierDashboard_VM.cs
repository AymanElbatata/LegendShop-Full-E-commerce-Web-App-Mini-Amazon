namespace AymanStore.PL.Models
{
    public class SupplierDashboard_VM
    {
        public string SupplierName { get; set; }
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public int PendingOrders { get; set; }
        public int CompletedOrders { get; set; }
        public int CanceledOrders { get; set; }
        public int ExpectedOrders { get; set; }
        public int ReviewsOrders { get; set; }
        public int ComplainProducts { get; set; }
        public int ComplainProductsNew { get; set; }
        public int TotalIncome { get; set; }
        public int CashBalance { get; set; }
    }
}
