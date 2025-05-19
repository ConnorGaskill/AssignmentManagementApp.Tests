using AssignmentManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagement.Core.Interfaces
{
    public interface IAssignmentService
    {
        public bool AddAssignment(Assignment assignment);

        public List<Assignment> ListAll();

        public List<Assignment> ListIncomplete();

        public Assignment FindAssignmentByTitle(string title);
        public Assignment FindAssignmentById(Guid id);
        public bool MarkAssignmentComplete(string title);
        public bool DeleteAssignment(string title);
        public bool UpdateAssignment(string oldTitle, string newTitle, string newDescription);
    }
}
