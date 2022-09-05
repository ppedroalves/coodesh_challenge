using Commerce.Data;
using Commerce.Models;
using Commerce.ViewModels;
using Commerce.ViewModels.ProductViewModels;
using Commerce.ViewModels.ProviderViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET: api/<ProductController>
        [HttpGet]
        public async  Task<IActionResult> GetAsync(
            [FromServices] Context _context)
        {

            var products = await
                _context
                .Products
                .AsNoTracking()
                .Include(x => x.Provider)
                .Include(x => x.Category)
                .Select( x => new ResultProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category.Name,
                    Provider = new EditorProviderViewModel { Name = x.Provider.Name, Email = x.Provider.Email },
                    Created = x.CreatedAt,
                    Description = x.Description,
                    Price = x.Price
                })
                .ToListAsync();


            return Ok(new ResultViewModel<List<ResultProductViewModel>>(products));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id,
            [FromServices] Context _context )
        {
            try
            {
                var product = await _context
                    .Products
                    .AsNoTracking()
                .Include(x => x.Provider)
                .Include(x => x.Category)
                .Select(x => new ResultProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category.Name,
                    Provider = new EditorProviderViewModel { Name = x.Provider.Name, Email = x.Provider.Email },
                    Created = x.CreatedAt,
                    Description = x.Description,
                    Price = x.Price
                })
                .FirstOrDefaultAsync(x => x.Id == id);
                    

                if (product == null)
                    return NotFound("Product not found");

                return Ok(new ResultViewModel<ResultProductViewModel>(product));
            }
            catch
            {
                return StatusCode(500, "Falha interna no servidor");
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] EditorProductViewModel model,
            [FromServices] Context _context)
        {

            try
            {
                var provider = await _context.Providers.FirstOrDefaultAsync(x => x.Id == model.Provider);
                if(provider == null)
                    return NotFound("Provider not found");
                var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == model.Category);
                if (category == null)
                    return NotFound("Category not found");

                var product = new Product
                {
                    Id = 0,
                    Category = category,
                    Provider = provider,
                    Name = model.Name,
                    CreatedAt = DateTime.Now,
                    Description = model.Description,
                    Price = model.Price };

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                return Created($"/product/{product.Id}", new ResultViewModel<Product>(product));


            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Could not include product");
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
