using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rodz.UI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;


using DES.Domain.Entities;
using Rodz.UI.Services;

namespace Rodz.UI.Areas.Admin.Pages
{
    public class CreateModel(ICategoryService categoryService, IProductService productService) : PageModel
    {
     
            public async Task<IActionResult> OnGet()
            {
                var categoryListData = await categoryService.GetCategoryListAsync();
                ViewData["CategoryId"] = new SelectList(categoryListData.Data, "Id",
                "GroupName");
                return Page();
            }
            [BindProperty]
            public Dessert dessert { get; set; } = default!;
            [BindProperty]
            public IFormFile? Image { get; set; }

            public async Task<IActionResult> OnPostAsync()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                await productService.CreateProductAsync(dessert, Image);
                return RedirectToPage("./Index");
            }
        }
    }
