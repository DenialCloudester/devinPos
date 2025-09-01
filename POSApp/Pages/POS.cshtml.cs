using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using POSApp.Data;
using POSApp.Models;
using System.Security.Claims;
using System.Text.Json;

namespace POSApp.Pages
{
    [Authorize]
    public class POSModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public POSModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> Products { get; set; } = new List<Product>();

        public async Task OnGetAsync()
        {
            Products = await _context.Products
                .Where(p => p.IsActive && p.StockQuantity > 0)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostProcessSaleAsync([FromBody] SaleRequest request)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return new JsonResult(new { success = false, message = "User not authenticated" });
                }

                foreach (var item in request.SaleItems)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product == null)
                    {
                        return new JsonResult(new { success = false, message = $"Product not found" });
                    }
                    
                    if (product.StockQuantity < item.Quantity)
                    {
                        return new JsonResult(new { success = false, message = $"Insufficient stock for {product.Name}" });
                    }
                }

                var sale = new Sale
                {
                    UserId = userId,
                    SaleDate = DateTime.UtcNow,
                    SubTotal = request.SubTotal,
                    TaxAmount = request.TaxAmount,
                    TotalAmount = request.TotalAmount,
                    PaymentMethod = request.PaymentMethod
                };

                _context.Sales.Add(sale);
                await _context.SaveChangesAsync();

                foreach (var item in request.SaleItems)
                {
                    var saleItem = new SaleItem
                    {
                        SaleId = sale.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        TotalPrice = item.TotalPrice
                    };

                    _context.SaleItems.Add(saleItem);

                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product != null)
                    {
                        product.StockQuantity -= item.Quantity;
                        product.UpdatedAt = DateTime.UtcNow;
                    }
                }

                await _context.SaveChangesAsync();

                return new JsonResult(new { success = true, saleId = sale.Id });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }
    }

    public class SaleRequest
    {
        public List<SaleItemRequest> SaleItems { get; set; } = new List<SaleItemRequest>();
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; } = "Cash";
    }

    public class SaleItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
