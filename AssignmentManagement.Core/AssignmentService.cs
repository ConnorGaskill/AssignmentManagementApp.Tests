using System;
using System.Collections.Generic;
using System.Linq;

namespace AssignmentManagement.Core
{
    public class AssignmentService : IAssignmentService
    {
        private readonly List<Assignment> _assignments = new();

        public bool AddAssignment(Assignment assignment)
        {
            if (_assignments.Any(a => a.Title.Equals(assignment.Title, StringComparison.OrdinalIgnoreCase)))
            {
                return false; // Duplicate title exists
            }

            _assignments.Add(assignment);
            return true;
        }

        public List<Assignment> ListAll()
        {
            return _assignments;
        }

        public List<Assignment> ListIncomplete()
        {
            List<Assignment> incomplete = new();

            if (_assignments.Count == 0 || _assignments == null)
                throw new ArgumentException("No assignments");

            return _assignments.Where(a => !a.IsCompleted).ToList();
        }

        // TODO: Implement method to find an assignment by title
        public Assignment? FindAssignmentByTitle(string title)
        {
            return _assignments.FirstOrDefault(a => a.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
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
            var assignment = FindAssignmentByTitle(title);
            if (assignment == null)
                return false;

            _assignments.Remove(assignment);
            return true;
        }

        // TODO: Implement method to update an assignment (title and description)
        public bool UpdateAssignment(string oldTitle, string newTitle, string newDescription)
        {
            var assignment = FindAssignmentByTitle(oldTitle);
            if (assignment == null)
                return false;

            if (!oldTitle.Equals(newTitle, StringComparison.OrdinalIgnoreCase) &&
                _assignments.Any(a => a.Title.Equals(newTitle, StringComparison.OrdinalIgnoreCase)))
            {
                return false; // Conflict
            }

            assignment.Update(newTitle, newDescription);
            return true;
        }
    }
}
