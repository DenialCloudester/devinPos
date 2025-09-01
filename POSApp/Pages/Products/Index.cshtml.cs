using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using POSApp.Data;
using POSApp.Models;

namespace POSApp.Pages.Products
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Products = await _context.Products
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
    }
}
