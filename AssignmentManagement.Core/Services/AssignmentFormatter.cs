using AssignmentManagement.Core.Interfaces;
using AssignmentManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagement.Core.Services
{
    public class AssignmentFormatter : IAssignmentFormatter
    {
        public string Format(List<Assignment> assignments)
        {
            return string.Join("\n", assignments.Select(a 
                => $"ID: [{a.Id}], Title: {a.Title}, Description: {a.Description}, Is Completed?: {a.IsCompleted}"));
        }
        public string Format(Assignment assignment)
        {
            return $"ID: [{assignment.Id}], Title: {assignment.Title}, Description: {assignment.Description}, Is Completed?: {assignment.IsCompleted}";
        }
    }
}
