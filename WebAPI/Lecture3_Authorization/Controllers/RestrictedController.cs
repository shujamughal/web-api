using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Lecture3_Authorization.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestrictedController : ControllerBase
    {
    [Authorize(Policy = "EmployeePolicy")]  
    [HttpGet("restricted-area")]
        public IActionResult GetRestrictedArea()
        {
            return Ok("Access granted to HR Employees users.");
        }
    }
}
