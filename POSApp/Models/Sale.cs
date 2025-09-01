using System.ComponentModel.DataAnnotations;

namespace POSApp.Models
{
    public class Sale
    {
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        public ApplicationUser? User { get; set; }
        
        [Required]
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal TotalAmount { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public decimal TaxAmount { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal SubTotal { get; set; }
        
        [StringLength(20)]
        public string PaymentMethod { get; set; } = "Cash";
        
        [StringLength(500)]
        public string? Notes { get; set; }
        
        public List<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }
}
