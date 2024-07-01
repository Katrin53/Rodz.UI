using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using DES.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Rodz.API.Data;

namespace Rodz.API.Data
{

    public static class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {

            // Uri проекта
            var uri = "https://localhost:7002/";
            // Получение контекста БД
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            ////Выполнение миграций
            //await context.Database.MigrateAsync();

            if (!context.Categories.Any() && !context.Dessert.Any())
            {
                var categories = new Category[]
            {
            new Category {GroupName="Ягодный",
            NormalizedName="Ягодный десерт"},
            new Category {GroupName="Фруктовый",
            NormalizedName="Фруктовый десерт"},
            new Category {GroupName="Творожный",
            NormalizedName="Творожный десерт"}
            };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();


               var _dessert = new List<Dessert>
        {
            new Dessert {Name="Пироженное с фруктами",
            Description="Десерт фруктовый",
            Image= uri + "Images/01.jpg",
            Category = categories.FirstOrDefault(c=>c.NormalizedName.Equals("Фруктовый десерт"))},

            new Dessert {Name="Пироженное с ягодами",
            Description="Десерт ягодный",
            Image= uri + "Images/02.jpg",
            Category = categories.FirstOrDefault(c=>c.NormalizedName.Equals("Ягодный десерт"))},

            new Dessert {Name="Пироженное с творогом",
            Description="Десерт творожный",
            Image= uri +"Images/03.jpg",
            Category = categories.FirstOrDefault(c=>c.NormalizedName.Equals("Творожный десерт"))},

            new Dessert {Name="Пироженное с фруктами",
            Description="Десерт фруктовый",
            Image= uri + "Images/04.jpg",
            Category = categories.FirstOrDefault(c=>c.NormalizedName.Equals("Фруктовый десерт"))},

            new Dessert {Name="Пироженное с ягодами",
            Description="Десерт ягодный",
            Image= uri + "Images/05.jpg",
            Category = categories.FirstOrDefault(c=>c.NormalizedName.Equals("Ягодный десерт"))},


            };

                await context.Dessert.AddRangeAsync(_dessert);
                await context.SaveChangesAsync();

            }
        }
    }
}

