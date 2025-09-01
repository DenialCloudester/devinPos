using System.ComponentModel.DataAnnotations;

namespace POSApp.Models
{
    public class SaleItem
    {
        public int Id { get; set; }
        
        [Required]
        public int SaleId { get; set; }
        
        public Sale? Sale { get; set; }
        
        [Required]
        public int ProductId { get; set; }
        
        public Product? Product { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal UnitPrice { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal TotalPrice { get; set; }
    }
}
