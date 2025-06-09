using AssignmentManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagement.Core.Interfaces
{
    public interface IAssignmentFormatter
    {
        public string Format(List<Assignment> assignments);
        public string Format(Assignment assignment);
        public string FormatPriorityToString(Priority priority);
        public Priority? FormatStringToPriority(string priority);

    }
}
