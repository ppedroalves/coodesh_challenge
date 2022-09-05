using Commerce.Data;
using Commerce.Models;
using Commerce.ViewModels;
using Commerce.ViewModels.ProviderViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
  

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromServices] Context _context)
        {

            var providers = await _context.Providers.ToListAsync();
            return Ok(new ResultViewModel<List<Provider>>(providers));
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EditorProviderViewModel model,
            [FromServices] Context _context)
        {
            try
            {

                var provider = new Provider
                {
                    Id = 0,
                    Name = model.Name,
                    Email = model.Email

                };

                await _context.Providers.AddAsync(provider);
                await _context.SaveChangesAsync();

                return Created($"/provider/{provider.Id}", new ResultViewModel<Provider>(provider));
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Could not include the provider");
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

    
    }
}
