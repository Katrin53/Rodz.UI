using DES.Domain.Entities;
using DES.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;

namespace Rodz.UI.Services
{
    
        public class MemoryProductService: IProductService
        {
            List<Dessert> _dessert;
            List<Category> _categories;
            IConfiguration _config;



            public MemoryProductService(ICategoryService categoryService, [FromServices] IConfiguration config)
            {
                _config = config;
                _categories = categoryService.GetCategoryListAsync()
                    .Result
                    .Data;

                SetupData();
            }



        /// <summary>
        /// Инициализация списков
        /// </summary>
        public void SetupData()
        {

            _dessert = new List<Dessert>
        {
            new Dessert {Id = 1, Name="Пироженное с фруктами",
            Description="Десерт фруктовый",
            Image="Images/01.jpg",
            CategoryId = _categories.Find(c=>c.NormalizedName.Equals("Фруктовый десерт")).Id},

            new Dessert {Id = 2, Name="Пироженное с ягодами",
            Description="Десерт ягодный",
            Image="Images/02.jpg",
            CategoryId = _categories.Find(c=>c.NormalizedName.Equals("Ягодный десерт")).Id},

            new Dessert {Id = 3, Name="Пироженное с творогом",
            Description="Десерт творожный",
            Image="Images/03.jpg",
            CategoryId = _categories.Find(c=>c.NormalizedName.Equals("Творожный десерт")).Id},

            new Dessert {Id = 4, Name="Пироженное с фруктами",
            Description="Десерт фруктовый",
            Image="Images/04.jpg",
            CategoryId = _categories.Find(c=>c.NormalizedName.Equals("Фруктовый десерт")).Id},

            new Dessert {Id = 5, Name="Пироженное с ягодами",
            Description="Десерт ягодный",
            Image="Images/05.jpg",
            CategoryId = _categories.Find(c=>c.NormalizedName.Equals("Ягодный десерт")).Id},


            };

        }
        Task<ResponseData<ListModel<Dessert>>> IProductService.GetProductListAsync(string? categoryNormalizedName, int pageNo=1)
        {


                // Создать объект результата
                var result = new ResponseData<ListModel<Dessert>>();

                // Id категории для фильрации
                int? categoryId = null;

                // если требуется фильтрация, то найти Id категории
                // с заданным categoryNormalizedName
                if (categoryNormalizedName != null)
                    categoryId = _categories
                    .Find(c =>
                    c.NormalizedName.Equals(categoryNormalizedName))
                    ?.Id;

                // Выбрать объекты, отфильтрованные по Id категории,
                // если этот Id имеется
                var data = _dessert
                .Where(d => categoryId == null || d.CategoryId.Equals(categoryId))?
                .ToList();

                // получить размер страницы из конфигурации
                int pageSize = _config.GetSection("ItemsPerPage").Get<int>();


                // получить общее количество страниц
                int totalPages = (int)Math.Ceiling(data.Count / (double)pageSize);

                // получить данные страницы
                var listData = new ListModel<Dessert>()
                {
                    Items = data.Skip((pageNo - 1) *
                pageSize).Take(pageSize).ToList(),
                    CurrentPage = pageNo,
                    TotalPages = totalPages
                };

                // поместить ранные в объект результата
                result.Data = listData;



                // Если список пустой
                if (data.Count == 0)
                {
                    result.Success = false;
                    result.ErrorMessage = "Нет объектов в выбраннной категории";
                }
                // Вернуть результат
                return Task.FromResult(result);

            }



        public Task<ResponseData<Dessert>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(int id, Dessert product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Dessert>> CreateProductAsync(Dessert product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }


    }
    }
