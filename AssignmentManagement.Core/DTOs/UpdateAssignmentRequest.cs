using AssignmentManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagement.Core.DTOs
{
    /// <summary>
    /// DTO for packaging Assignment update requests to be passed 
    /// to the Assignment service.
    /// </summary>
    public class UpdateAssignmentRequest
    {
        public string OldTitle { get; set; }
        public string? NewTitle { get; set; }
        public string? NewDescription { get; set; }
        public Priority? NewPriority { get; set; }
        public string? NewNotes { get; set; }

        public UpdateAssignmentRequest(string oldTitle)
            {
                OldTitle = oldTitle;
            }
    }

}
