using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rodz.UI.Data;

using DES.Domain.Entities;

namespace Rodz.UI.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}