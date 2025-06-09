using AssignmentManagement.Core.DTOs;
using AssignmentManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagement.Core.Interfaces
{
    /// <summary>
    /// Interface for the Assignment Service used as a middle 
    /// layer to communicate with the Assignment class.
    /// 
    /// Responsible for repository manipulation, logging, and request handling.
    /// </summary>
    public interface IAssignmentService
    {
        public bool AddAssignment(Assignment assignment);

        public List<Assignment> ListAll();

        public List<Assignment> ListIncomplete();

        public Assignment FindAssignmentByTitle(string title);
        public Assignment FindAssignmentById(Guid id);
        public bool MarkAssignmentComplete(string title);
        public bool DeleteAssignment(string title);
        public bool UpdateAssignment(UpdateAssignmentRequest request);
        public string FormatPriorityToString(Priority priority);
        public Priority? FormatStringToPriority(string priority);

    }
}
