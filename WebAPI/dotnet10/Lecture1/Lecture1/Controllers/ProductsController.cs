using Lecture1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lecture1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop", Price = 999.99M },
        new Product { Id = 2, Name = "Smartphone", Price = 499.99M }
    };

        [HttpGet(Name = "GetAllProducts")]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return Ok(_products);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet("expensive", Name = "GetExpensiveProducts")]
        public ActionResult<IEnumerable<Product>> GetExpensiveProducts()
        {
            var expensiveProducts = _products.Where(p => p.Price > 500).ToList();
            if (!expensiveProducts.Any()) return NotFound("No expensive products found.");
            return Ok(expensiveProducts);
        }

        [HttpPost(Name = "CreateProduct")]
        public ActionResult Create(Product product)
        {
            product.Id = _products.Count + 1;
            _products.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPost("route-example", Name = "CreateProductWithRoute")]
        public ActionResult CreateWithRoute(Product product)
        {
            product.Id = _products.Count + 1;
            _products.Add(product);
            return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
        }

        [HttpPut("{id}", Name = "UpdateProduct")]
        public ActionResult Update(int id, Product updatedProduct)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteProduct")]
        public ActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            _products.Remove(product);
            return NoContent();
        }

        [HttpGet("cheap", Name = "GetCheapProducts")]
        public ActionResult<IEnumerable<Product>> GetCheapProducts()
        {
            var cheapProducts = _products.Where(p => p.Price <= 500).ToList();
            return Ok(cheapProducts);
        }
    }
}