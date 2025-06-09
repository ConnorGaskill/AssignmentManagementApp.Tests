using AssignmentManagement.Core.Interfaces;
using AssignmentManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagement.Core.Services
{
    /// <summary>
    /// Object representing the formatter used by the assignment service.
    /// Responsible for formatting Assignments and related fields into other data types.
    /// </summary>
    public class AssignmentFormatter : IAssignmentFormatter
    {
        public string Format(List<Assignment> assignments)
        {
            return string.Join("\n", assignments.Select(a 
                => $"ID: [{a.Id}], ({a.Priority}) Title: {a.Title}, Description: {a.Description}, Is Completed?: {a.IsCompleted}, Notes: {a.Notes}"));
        }
        public string Format(Assignment assignment)
        {
            return $"ID: [{assignment.Id}], ({assignment.Priority}) Title: {assignment.Title}, Description: {assignment.Description}, Is Completed?: {assignment.IsCompleted}, Notes: {assignment.Notes}";
        }

        public string FormatPriorityToString(Priority priority) {

            switch (priority) { 
            
                case Priority.Low:
                    return "Low";
                case Priority.Medium:
                    return "Medium";
                default:
                    return "High";

            }
        }

        public Priority? FormatStringToPriority(string priority) {

            switch (priority.Trim().ToUpper())
            {
                case "HIGH":
                case "H":
                    return Priority.High;
                case "MEDIUM":
                case "M":
                    return Priority.Medium;
                case "LOW":
                case "L":
                    return Priority.Low;
                default:
                    return null;
            }

        }

    }
}
