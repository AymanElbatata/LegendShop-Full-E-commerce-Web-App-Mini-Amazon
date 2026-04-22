using AymanStore.DAL.BaseEntity;
using System.ComponentModel.DataAnnotations;

namespace AymanStore.PL.Models
{
    public class ConfirmationOrder_VM 
    {
        public string? ItemName { get; set; } = string.Empty;
        public string? Barcode { get; set; } = string.Empty;
        public int? Quantity { get; set; } = 0;
        public int? Price { get; set; } = 0;
        public int? Shipping { get; set; } = 0;
        public string? ShippingStatus { get; set; } = string.Empty;
    }
}
