using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using POSApp.Data;
using POSApp.Models;

namespace POSApp.Pages.Sales
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Sale Sale { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.User)
                .Include(s => s.SaleItems)
                    .ThenInclude(si => si.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (sale == null)
            {
                return NotFound();
            }
            else
            {
                Sale = sale;
            }
            return Page();
        }
    }
}
