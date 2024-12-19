using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lecture3_webpi_jwt_authentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult GetProducts()
        {
            var products = new List<string> { "Product1", "Product2", "Product3" };
            return Ok(products);
        }
    }
}
