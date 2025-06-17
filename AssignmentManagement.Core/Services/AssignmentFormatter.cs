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
        /// <summary>
        /// Formats a list of assignments
        /// </summary>
        /// <param name="assignments">The list of assignments</param>
        /// <returns>A string containing all formatted assignments</returns>
        public string Format(List<Assignment> assignments)
        {
            return string.Join("\n", assignments.Select(a 
                => $"ID: [{a.Id}], ({FormatPriorityToString(a.Priority)}), Title: {a.Title}, Description: {a.Description}, Is Completed?: {a.IsCompleted}, Notes: {a.Notes}"));
        }
        /// <summary>
        /// Formats a single assignment
        /// </summary>
        /// <param name="assignment">The assignment</param>
        /// <returns>A string containing the formatted assignment</returns>
        public string Format(Assignment assignment)
        {
            return $"ID: [{assignment.Id}], ({FormatPriorityToString(assignment.Priority)}) Title: {assignment.Title}, Description: {assignment.Description}, Is Completed?: {assignment.IsCompleted}, Notes: {assignment.Notes}";
        }
        /// <summary>
        /// Formats an assignment's priority into a string
        /// </summary>
        /// <param name="priority">The assignment's priority</param>
        /// <returns>a string containing the assignment's priority</returns>
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
        /// <summary>
        /// Formats a string into an assignment priority
        /// </summary>
        /// <param name="priority">A string being converted into a priority</param>
        /// <returns>
        /// A priority representing given string
        /// Null if the given string cannot be converted
        /// </returns>
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
