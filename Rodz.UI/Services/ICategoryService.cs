using DES.Domain.Entities;
using DES.Domain.Models;


namespace Rodz.UI.Services
{
    public interface ICategoryService
    {
        /// <summary>
        /// Получение списка всех категорий
        /// </summary>
        /// <returns></returns>
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}
