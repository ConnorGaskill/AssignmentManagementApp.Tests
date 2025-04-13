using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xunit;
using Assert = Xunit.Assert;

namespace AssignmentManagementApp.Tests
{
    public class AssignmentTests
    {
        [Fact]
        public void Assignment_Should_Have_a_Title()
        {
            var assignment = new Assignment
            {
                // Title of the assignment
                Title = "Default Title" // Required by annotation
            };
            Assert.NotNull(assignment.Title);
        }
    }
}
