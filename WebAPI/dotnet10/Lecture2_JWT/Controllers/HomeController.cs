using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lecture2_JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [Authorize]
        [HttpGet("secure-data")]
        public IActionResult SecureData()
        {
            return Ok("This is a protected resource.");
        }
    }
}
