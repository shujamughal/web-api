using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RateLimitingExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(new[] { "Product1", "Product2", "Product3" });
        }

        [HttpGet("details/{id}")]
        public IActionResult GetProductDetails(int id)
        {
            return Ok(new { Id = id, Name = $"Product{id}", Price = 100 + id });
        }
    }
}
