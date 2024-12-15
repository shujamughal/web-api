using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Odata_Lecture1.Models;

namespace Odata_Lecture1.Controllers
{

    public class CategoriesController : ODataController
    {
        

        public CategoriesController()
        {
            
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(new Category { Id = 1, Name = "Electronics", Description="this is electronics" });
        }

       
    }
}
