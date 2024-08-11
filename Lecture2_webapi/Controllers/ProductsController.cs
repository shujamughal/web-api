using Lecture2_webapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lecture2_webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static List<Product> _products = new List<Product>
    {
        new Product { Id = 1, Name = "Laptop", Price = 999.99m },
        new Product { Id = 2, Name = "Smartphone", Price = 499.99m }
    };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_products); // 200 OK
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); // 404 Not Found
            }
            return Ok(product); // 200 OK
        }

        [HttpPost]
        public IActionResult Create(Product newProduct)
        {
            if (newProduct == null || string.IsNullOrEmpty(newProduct.Name))
            {
                return BadRequest("Invalid product data."); // 400 Bad Request
            }

            if (_products.Any(p => p.Id == newProduct.Id))
            {
                return Conflict("Product with this ID already exists."); // 409 Conflict
            }

            _products.Add(newProduct);
            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct); // 201 Created
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); // 404 Not Found
            }

            _products.Remove(product);
            return NoContent(); // 204 No Content
        }

    }
}