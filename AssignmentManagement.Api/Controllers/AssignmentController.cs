using AssignmentManagement.Core.DTOs;
using AssignmentManagement.Core.Interfaces;
using AssignmentManagement.Core.Models;
using AssignmentManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AssignmentManagement.API.Controllers
{
    /// <summary>
    /// Controller for the API.
    /// 
    /// Responsible for translating Assignment data into HTTP requests.
    /// </summary>
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
        //GET api/<AssignmentController>/5
        [HttpGet("{id:Guid}")]
        public IActionResult Get(Guid id)
        {
            var assignment = _assignmentService.FindAssignmentById(id);
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
        [HttpPut]
        public IActionResult Put([FromBody] UpdateAssignmentRequest request)
        {
            bool success = _assignmentService.UpdateAssignment(request);
            if (!success)
            {
                return NotFound($"Assignment with title '{request.OldTitle}' not found or conflict occurred.");
            }

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
