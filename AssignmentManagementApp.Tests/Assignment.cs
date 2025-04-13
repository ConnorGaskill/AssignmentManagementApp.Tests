using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagementApp.Tests
{
    public class Assignment
    {
        [Required(ErrorMessage = "Assignment title is required.")]
        public string Title { get; set; }
    }
}
