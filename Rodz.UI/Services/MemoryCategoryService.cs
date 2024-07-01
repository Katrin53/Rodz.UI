
using DES.Domain.Entities;
using DES.Domain.Models;


namespace Rodz.UI.Services
{
    public class MemoryCategoryService : ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var categories = new List<Category>
            {
            new Category {Id=1, GroupName="Ягодный",
            NormalizedName="Ягодный десерт"},
            new Category {Id=2, GroupName="Фруктовый",
            NormalizedName="Фруктовый десерт"},
            new Category {Id=2, GroupName="Творожный",
            NormalizedName="Творожный десерт"}

            };
            var result = new ResponseData<List<Category>>();
            result.Data = categories;
            return Task.FromResult(result);
        }

       
    }
}
