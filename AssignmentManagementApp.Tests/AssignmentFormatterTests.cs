using AssignmentManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AssignmentManagement.Core.Services;

namespace AssignmentManagement.Tests
{
    public class AssignmentFormatterTests
    {
        [Fact]
        public void Format_ShouldFormatAssignmentCorrectly()
        {
            var formatter = new AssignmentFormatter();
            var assignment = new Assignment("Software Engineering", "Write more tests");

            var formattedAssignment = formatter.Format(assignment);

            Assert.Equal(
                $"ID: [{assignment.Id}], ({assignment.Priority}) Title: {assignment.Title}, " +
                $"Description: {assignment.Description}, " +
                $"Is Completed?: {assignment.IsCompleted}, " +
                $"Notes: {assignment.Notes}", formattedAssignment);


        }
        [Fact]
        public void Format_ShouldFormatAssignmentListCorrectly()
        {
            
            var formatter = new AssignmentFormatter();
            var assignments = new List<Assignment>
            {
                new Assignment("Software Engineering", "Do stuff"),
                new Assignment("Software Engineering", "Do stuff")
            };

            
            var formattedAssignments = formatter.Format(assignments);

            
            var expectedOutput = string.Join("\n", assignments.Select(a =>
                $"ID: [{a.Id}], ({a.Priority}) Title: {a.Title}, " +
                $"Description: {a.Description}, " +
                $"Is Completed?: {a.IsCompleted}, " +
                $"Notes: {a.Notes}"));

            Assert.Equal(expectedOutput, formattedAssignments);
        }

    }
}
