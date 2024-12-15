using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Odata_Lecture1.Models;

namespace Odata_Lecture1.Controllers
{

    public class CategoriesController : ODataController
    {
        //protected readonly NorthwindContext db;

        public CategoriesController()
        {
            //this.db = db;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(new Category { Id = 1, Name = "c1dsfsaf", Description="cat des" });
        }

       
    }
}
