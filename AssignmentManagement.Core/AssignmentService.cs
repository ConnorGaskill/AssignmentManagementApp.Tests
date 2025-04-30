using System;
using System.Collections.Generic;
using System.Linq;

namespace AssignmentManagement.Core
{
    public class AssignmentService
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
            return new List<Assignment>(_assignments);
        }

        public List<Assignment> ListIncomplete()
        {
            return _assignments.Where(a => !a.IsCompleted).ToList();
        }

        // TODO: Implement method to find an assignment by title
        public Assignment FindAssignmentByTitle(string title)
        {
            throw new NotImplementedException();
        }

        // TODO: Implement method to mark an assignment complete
        public bool MarkAssignmentComplete(string title)
        {
            throw new NotImplementedException();
        }

        // TODO: Implement method to delete an assignment by title
        public bool DeleteAssignment(string title)
        {
            throw new NotImplementedException();
        }

        // TODO: Implement method to update an assignment (title and description)
        public bool UpdateAssignment(string oldTitle, string newTitle, string newDescription)
        {
            throw new NotImplementedException();
        }
    }
}
