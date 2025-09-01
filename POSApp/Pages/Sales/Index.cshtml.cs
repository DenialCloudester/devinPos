using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using POSApp.Data;
using POSApp.Models;

namespace POSApp.Pages.Sales
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Sale> Sales { get; set; } = default!;
        public decimal TotalSales { get; set; }
        public decimal TodaySales { get; set; }
        public decimal AverageSale { get; set; }
        public int TotalTransactions { get; set; }

        public async Task OnGetAsync()
        {
            Sales = await _context.Sales
                .Include(s => s.User)
                .Include(s => s.SaleItems)
                .OrderByDescending(s => s.SaleDate)
                .ToListAsync();

            TotalSales = Sales.Sum(s => s.TotalAmount);
            TotalTransactions = Sales.Count;
            AverageSale = TotalTransactions > 0 ? TotalSales / TotalTransactions : 0;
            
            var today = DateTime.Today;
            TodaySales = Sales
                .Where(s => s.SaleDate.Date == today)
                .Sum(s => s.TotalAmount);
        }
    }
}
