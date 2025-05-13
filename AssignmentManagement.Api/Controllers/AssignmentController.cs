using AssignmentManagement.Core;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AssignmentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentController(IAssignmentService assignmentService)
        {
            this._assignmentService = assignmentService;
        }

        // GET: api/<AssignmentController>
        [HttpGet]
        public IActionResult GetAll() => Ok(_assignmentService.ListAll());

        // GET api/<AssignmentController>/5
        [HttpGet("{title}")]
        public IActionResult Get(string title)
        {
            var assignment = _assignmentService.FindAssignmentByTitle(title);
            return assignment is null ? NotFound() : Ok(assignment);
        }

        // POST api/<AssignmentController>
        [HttpPost]
        public IActionResult Create(Assignment assignment)
        {
            var createdAssignment = _assignmentService.AddAssignment(assignment);
            return Ok(createdAssignment);
        }

        // PUT api/<AssignmentController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string oldTitle, string newTitle, string newDescription, [FromBody] Assignment assignemnt)
        {
            _assignmentService.UpdateAssignment(oldTitle, newTitle, newDescription);
            return NoContent();
        }

        // DELETE api/<AssignmentController>/5
        [HttpDelete("{title}")]
        public IActionResult Delete(string title)
        {
            _assignmentService.DeleteAssignment(title);
            return NoContent();
        }
    }
}
