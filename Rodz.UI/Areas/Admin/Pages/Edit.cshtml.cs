using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rodz.UI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


using DES.Domain.Entities;

namespace Rodz.UI.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _environment;
        public EditModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }


        [BindProperty]
        public Dessert dessert { get; set; } = default!;
        [BindProperty]
        public IFormFile Image { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Dessert == null)
            {
                return NotFound();
            }

            var dish = await _context.Dessert.FirstOrDefaultAsync(m => m.Id == id);
            if (dish == null)
            {
                return NotFound();
            }
            dessert = dessert;
            ViewData["GroupId"] = new SelectList(_context.Dessert, "GroupId", "GroupName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Image != null)
            {
                var fileName = $"{dessert.Id}" +
                Path.GetExtension(Image.FileName);
                dessert.Image = fileName;
                var path = Path.Combine(_environment.WebRootPath, "Images",
                fileName);
                using (var fStream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(fStream);
                }
            }


            _context.Attach(dessert).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DessertExists(dessert.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DessertExists(int id)
        {
            return _context.Dessert.Any(e => e.Id == id);
        }
    }
}