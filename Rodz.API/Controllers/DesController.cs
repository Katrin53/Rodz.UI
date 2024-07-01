using DES.Domain.Entities;
using DES.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rodz.API.Data;

namespace Rodz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DesController(AppDbContext context, IWebHostEnvironment _environment)
        {
            _context = context;

        }


        // GET: api/Dishes
        [HttpGet]
        public async Task<ActionResult<ResponseData<ListModel<Dessert>>>> GetProductListAsync(
              string? category,
              int pageNo = 1,
              int pageSize = 3)
        {
            // Создать объект результата
            var result = new ResponseData<ListModel<Dessert>>();

            // Фильтрация по категории загрузка данных категории
            var data = _context.Dessert
            .Include(d => d.Category)
            .Where(d => String.IsNullOrEmpty(category)
            || d.Category.NormalizedName.Equals(category));

            // Подсчет общего количества страниц
            int totalPages = (int)Math.Ceiling(data.Count() / (double)pageSize);
            if (pageNo > totalPages)
                pageNo = totalPages;

            // Создание объекта ProductListModel с нужной страницей данных
            var listData = new ListModel<Dessert>()
            {
                Items = await data
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };
            // поместить данные в объект результата
            result.Data = listData;
            // Если список пустой
            if (data.Count() == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбранной категории";
            }
            return result;
        }
        // GET: api/
        [HttpGet("{id}")]
        public async Task<ActionResult<Dessert>> GetDessert(int id)
        {
            var dessert = await _context.Dessert.FindAsync(id);

            if (dessert == null)
            {
                return NotFound();
            }

            return dessert;
        }

        // PUT: api/
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDessert(int id, Dessert dessert)
        {
            if (id != dessert.Id)
            {
                return BadRequest();
            }

            _context.Entry(dessert).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DessertExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dessert>> PostDessert(Dessert dessert)
        {
            _context.Dessert.Add(dessert);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDessert", new { id = dessert.Id }, dessert);
        }

        // DELETE: api/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDessert(int id)
        {
            var dessert = await _context.Dessert.FindAsync(id);
            if (dessert == null)
            {
                return NotFound();
            }

            _context.Dessert.Remove(dessert);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DessertExists(int id)
        {
            return _context.Dessert.Any(e => e.Id == id);
        }

        [HttpPost("{id}")]

        public async Task<IActionResult> SaveImage(int id, IFormFile image, [FromServices] IWebHostEnvironment env)
        {
            // Найти объект по Id
            var dessert = await _context.Dessert.FindAsync(id);
            if (dessert == null)
            {
                return NotFound();
            }

            // Путь к папке wwwroot/Images
            var imagesPath = Path.Combine(env.WebRootPath, "Images");

            // получить случайное имя файла
            var randomName = Path.GetRandomFileName();

            // получить расширение в исходном файле
            var extension = Path.GetExtension(image.FileName);

            // задать в новом имени расширение как в исходном файле
            var fileName = Path.ChangeExtension(randomName, extension);

            // полный путь к файлу
            var filePath = Path.Combine(imagesPath, fileName);

            // создать файл и открыть поток для записи
            using var stream = System.IO.File.OpenWrite(filePath);

            // скопировать файл в поток
            await image.CopyToAsync(stream);

            // получить Url хоста
            var host = "https://" + Request.Host;

            // Url файла изображения
            var url = $"{host}/Images/{fileName}";

            // Сохранить url файла в объекте
            dessert.Image = url;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

