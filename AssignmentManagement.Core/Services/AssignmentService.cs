using AssignmentManagement.Core.Interfaces;
using AssignmentManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssignmentManagement.Core.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly List<Assignment> _assignments = new();

        private readonly IAppLogger _logger;

        private readonly IAssignmentFormatter _formatter;

        public AssignmentService(IAppLogger logger, IAssignmentFormatter formatter)
        {
            _logger = logger;
            _formatter = formatter;
        }

        public bool AddAssignment(Assignment assignment)
        {
            _logger.Log("Adding assignment...");
            if (_assignments.Any(a => a.Title.Equals(assignment.Title, StringComparison.OrdinalIgnoreCase)))
            {
                _logger.Log($"Assignment already exists:\n{_formatter.Format(assignment)}\n");
                return false; // Duplicate title exists
            }

            _assignments.Add(assignment);
            _logger.Log("Assignment added");
            return true;
        }

        public List<Assignment> ListAll()
        {
            _logger.Log("Listing assignments...");
            return _assignments;
        }

        public List<Assignment> ListIncomplete()
        {
            _logger.Log("Listing incomplete assignments...");
            List<Assignment> incomplete = new();

            if (_assignments.Count == 0 || _assignments == null)
                throw new ArgumentException("No assignments");

            return _assignments.Where(a => !a.IsCompleted).ToList();
        }

        // TODO: Implement method to find an assignment by title
        public Assignment? FindAssignmentByTitle(string title)
        {
            return _assignments.FirstOrDefault(a => a.Title.Equals(
                title, StringComparison.OrdinalIgnoreCase));
        }

        public Assignment? FindAssignmentById(Guid id)
        {
            return _assignments.FirstOrDefault(a => a.Id == id);
        }

        // TODO: Implement method to mark an assignment complete
        public bool MarkAssignmentComplete(string title)        
        {
            var assignment = FindAssignmentByTitle(title);

            if (assignment == null)
                return false;
            assignment.MarkComplete();
            return true;
        }

        // TODO: Implement method to delete an assignment by title
        public bool DeleteAssignment(string title)
        {
            _logger.Log("Finding assignment for deletion...");
            var assignment = FindAssignmentByTitle(title);
            if (assignment == null)
            {
                _logger.Log($"Assignment \"{title}\" not found");
                return false;
            }

            _assignments.Remove(assignment);
            _logger.Log("Assignment deleted");
            return true;
        }

        // TODO: Implement method to update an assignment (title and description)
        public bool UpdateAssignment(string oldTitle, string newTitle, string newDescription)
        {
            _logger.Log("finding assignment for update...");
            var assignment = FindAssignmentByTitle(oldTitle);
            if (assignment == null)
            {
                _logger.Log("No assignments");
                return false;
            }

            if (!oldTitle.Equals(newTitle, StringComparison.OrdinalIgnoreCase) &&
                _assignments.Any(a => a.Title.Equals(newTitle, StringComparison.OrdinalIgnoreCase)))
            {
                _logger.Log($"Assignment \"{oldTitle}\" not found");
                return false; // Conflict
            }

            assignment.Update(newTitle, newDescription);
            _logger.Log("assignment updated");
            return true;
        }

    }
}
