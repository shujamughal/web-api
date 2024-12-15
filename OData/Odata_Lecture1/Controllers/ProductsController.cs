using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Odata_Lecture1.Models;

namespace Odata_Lecture1.Controllers
{

    public class ProductsController : ODataController
    {
        private static readonly List<Product> Products = new List<Product>
    {
        new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 1200, InStock = true },
        new Product { Id = 2, Name = "Headphones", Category = "Electronics", Price = 200, InStock = true },
        new Product { Id = 3, Name = "Coffee Maker", Category = "Home Appliances", Price = 80, InStock = false }
    };

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(Products);
        }
    }
}
