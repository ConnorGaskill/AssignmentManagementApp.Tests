using AssignmentManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagement.Core.Interfaces
{
    /// <summary>
    /// Interface for the formatter used by the assignment service.
    /// Responsible for formatting Assignments and related fields into other data types.
    /// </summary>
    public interface IAssignmentFormatter
    {
        public string Format(List<Assignment> assignments);
        public string Format(Assignment assignment);
        public string FormatPriorityToString(Priority priority);
        public Priority? FormatStringToPriority(string priority);

    }
}
