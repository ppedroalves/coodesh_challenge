using Commerce.Data;
using Commerce.Models;
using Commerce.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {


        [HttpGet("")]
        public async Task<IActionResult> GetAsync(
            [FromServices] Context  _context)
        {

            var categories = await _context.Categories.ToListAsync();
            return Ok(new ResultViewModel<List<Category>>(categories));
        }



        [HttpPost]
        public async Task<IActionResult> PostAsync(
            [FromServices] Context _context,
            [FromBody] EditorCategoryViewModel model)
        {
            try
            {

                var category = new Category
                {
                    Id = 0,
                    Name = model.Name,
                    Slug = model.Name.ToLower(),
                    Description = model.Description,
                    CreatedAt = DateTime.Now

                };

                await _context.Categories.AddAsync(category);
                await  _context.SaveChangesAsync();

                return Created($"/category/{category.Id}", new ResultViewModel<Category>(category));
            }
            catch(DbUpdateException ex)
            {
                return BadRequest("Could not include category");
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

   
    }
}
