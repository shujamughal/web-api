using Lecture_2_Odata.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace Lecture_2_Odata.Controllers
{
    public class CategoriesController : ODataController
    {
        private static readonly List<Category> Categories = new List<Category>
        {
            new Category { Id = 1, Name = "Electronics" },
            new Category { Id = 2, Name = "Home Appliances" },
            new Category { Id = 3, Name = "Books" }
        };

        // GET: odata/Categories
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(Categories);
        }

        // GET: odata/Categories(1)
        [EnableQuery]
        public IActionResult Get(int key)
        {
            var category = Categories.FirstOrDefault(c => c.Id == key);
            return category != null ? Ok(category) : NotFound();
        }

        // POST, PUT, DELETE methods can be added similarly if required.
    }
}