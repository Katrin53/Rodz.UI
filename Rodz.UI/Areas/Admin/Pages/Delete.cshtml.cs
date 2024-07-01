using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DES.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rodz.UI.Data;



namespace Rodz.UI.Areas.Admin.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Dessert dessert { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Dessert == null)
            {
                return NotFound();
            }

            var dessert = await _context.Dessert.FirstOrDefaultAsync(m => m.Id == id);

            if (dessert == null)
            {
                return NotFound();
            }
            else
            {
                dessert = dessert;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Dessert == null)
            {
                return NotFound();
            }
            var dessert = await _context.Dessert.FindAsync(id);

            if (dessert != null)
            {
                dessert = dessert;
                _context.Dessert.Remove(dessert);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
