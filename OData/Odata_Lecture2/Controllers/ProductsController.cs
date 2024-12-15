using Lecture_2_Odata.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace Lecture_2_Odata.Controllers
{
    public class ProductsController : ODataController
    {
        private static readonly List<Product> Products = new List<Product>
{
    new Product { Id = 1, Name = "Laptop", Price = 1200, InStock = true, CategoryId = 1, Category = new Category { Id = 1, Name = "Electronics" } },
    new Product { Id = 2, Name = "Headphones", Price = 200, InStock = true, CategoryId = 1, Category = new Category { Id = 1, Name = "Electronics" } },
    new Product { Id = 3, Name = "Coffee Maker", Price = 80, InStock = false, CategoryId = 2, Category = new Category { Id = 2, Name = "Home Appliances" } }
};


        // GET: odata/Products
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(Products);
        }

        // GET: odata/Products(1)
        [EnableQuery]
        public IActionResult Get(int key)
        {
            var product = Products.FirstOrDefault(p => p.Id == key);
            return product != null ? Ok(product) : NotFound();
        }

        // POST: odata/Products
        public IActionResult Post([FromBody] Product product)
        {
            product.Id = Products.Max(p => p.Id) + 1;
            Products.Add(product);
            return Created(product);
        }

        // PUT: odata/Products(1)
        public IActionResult Put(int key, [FromBody] Product updatedProduct)
        {
            var product = Products.FirstOrDefault(p => p.Id == key);
            if (product == null) return NotFound();

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.InStock = updatedProduct.InStock;
            product.CategoryId = updatedProduct.CategoryId;

            return Updated(product);
        }

        // DELETE: odata/Products(1)
        public IActionResult Delete(int key)
        {
            var product = Products.FirstOrDefault(p => p.Id == key);
            if (product == null) return NotFound();

            Products.Remove(product);
            return NoContent();
        }

        [HttpGet("expensiveProducts")]
        [EnableQuery]

        public IActionResult GetExpensiveProducts()
        {
            var expensiveProducts = Products.Where(p => p.Price > 100);
            return Ok(expensiveProducts);
        }
    }
}