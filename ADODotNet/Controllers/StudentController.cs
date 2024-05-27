using ADODotNet.Models;
using ADODotNet.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADODotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentsServices studentservice;
        public StudentController(IStudentsServices studentsServices)
        {
            this.studentservice = studentsServices;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var result = studentservice.GetStudents();
            if (result == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetbyId(int id)
        {
            var result = studentservice.GetbyId(id);
            if (result == null) 
            {
                return BadRequest("Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddStudents(Students student)
        {
            student.id = studentservice.GetStudents().OrderByDescending(s => s.id).FirstOrDefault().id + 1;
            studentservice.AddStudents(student);
            return Ok();
        }

        [HttpPut]
        public IActionResult EditStudent(int id, Students student)
        {
            studentservice.UpdateStudents(id, student);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            studentservice.RemoveStudents(id);
            return Ok();
        }
    }
}
