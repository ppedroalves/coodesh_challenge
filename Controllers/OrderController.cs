using Commerce.Data;
using Commerce.Models;
using Commerce.ViewModels;
using Commerce.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {


  
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(
            [FromRoute] int id, [FromServices] Context _context)
        {

            try
            {

                var order =await  _context
                    .Orders
                    .AsNoTracking()
                    .Include(x => x.Itens)
                    .ThenInclude(x => x.Product)
                    .Select(x => new ResultOrderViewModel
                    {
                        Id = x.Id,
                        Total = x.Total,
                        CreatedAt = x.CreatedDate,
                        Itens = ConvertItens(x.Itens)
                    }
                    )
                    .FirstOrDefaultAsync(x => x.Id == id);

                if(order == null)
                    return NotFound("Order not found");

                return Ok (new ResultViewModel<ResultOrderViewModel>(order));

            } catch(Exception ex)
            {
                return StatusCode(500, "Falha interna no servidor");
            }
        }

        private static List<ResultItensViewModel> ConvertItens(IList<Itens> itens)
        {
            List<ResultItensViewModel> result = new();
            foreach (Itens iten in itens)
            {
                var resultItens = new ResultItensViewModel
                {
                    Product = iten.Product.Name,
                    Amount = iten.Amount

                };

                result.Add(resultItens);
            }

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] EditOrderViewModel model,
            [FromServices] Context _context)
        {
            try
            {
                var order = new Order
                {
                    Id = 0,
                    CreatedDate = DateTime.Now,
                };
                float total = 0.0f;
                List<Itens> itens = new();


                if(model.Itens == null)
                    return BadRequest("The order is empty");

                foreach (ItensViewModel item in model.Itens)
                {
                    var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == item.Product);
                    if (product == null)
                        return BadRequest("Product not found");

                    Itens i = new Itens();
                    i.Product = product;
                    i.Order = order;
                    i.Amount = item.Amount;
                    total += product.Price * item.Amount;

                    itens.Add(i);

                }

                order.Itens = itens;
                order.Total = total;

                await _context.Orders.AddAsync(order);
                await _context.Itens.AddRangeAsync(itens);
                await _context.SaveChangesAsync();

                return Created($"/order/{order.Id}", new ResultViewModel<Order>(order));


            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Could not include the order");
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
