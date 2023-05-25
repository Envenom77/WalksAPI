using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Walks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[] { "John", "Don" };

            return Ok(studentNames);
        }
    }
}
