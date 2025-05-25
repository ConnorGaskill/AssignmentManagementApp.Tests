namespace AssignmentManagementApp.Tests
{
    using Xunit;
    using AssignmentManagement.Core;
    using AssignmentManagement.Core.Services;
    using AssignmentManagement.Core.Models;
    using Moq;
    using AssignmentManagement.Core.Interfaces;
    using AssignmentManagement.Core.DTOs;

    public class AssignmentServiceTests
    {
        [Fact]
        public void ListIncomplete_ShouldReturnOnlyAssignmentsThatAreNotCompleted()
        {
            // Arrange
            var mockLogger = new Mock<ConsoleAppLogger>();
            var mockFormatter = new Mock<AssignmentFormatter>();
            var service = new AssignmentService(mockLogger.Object, mockFormatter.Object);

            var a1 = new Assignment("Incomplete Task", "Do something");
            var a2 = new Assignment("Completed Task", "Do something else");
            a2.MarkComplete();

            service.AddAssignment(a1);
            service.AddAssignment(a2);

            // Act
            var result = service.ListIncomplete();

            // Assert
            // This will fail until students implement ListIncomplete()
            Assert.Single(result);
            Assert.Equal("Incomplete Task", result[0].Title);
        }
        [Fact]
        public void ListIncomplete_EmptyListShouldThrowException()
        {
            var mockLogger = new Mock<ConsoleAppLogger>();
            var mockFormatter = new Mock<AssignmentFormatter>();
            var service = new AssignmentService(mockLogger.Object, mockFormatter.Object);

            // Calling ListIncomplete without any assignments
            Assert.Throws<ArgumentException>(() => service.ListIncomplete());
        }
        [Fact]
        public void ListIncomplete_ShouldReturnAllAssignmentsThatAreNotCompleted()
        {
            var mockLogger = new Mock<ConsoleAppLogger>();
            var mockFormatter = new Mock<AssignmentFormatter>();
            var service = new AssignmentService(mockLogger.Object, mockFormatter.Object);

            var a1 = new Assignment("Incomplete Task", "Do something");
            var a2 = new Assignment("Completed Task", "Do something else");
            var a3 = new Assignment("Unfinished Task", "Do something cool");
            a2.MarkComplete();

            service.AddAssignment(a1);
            service.AddAssignment(a2);
            service.AddAssignment(a3);

            var result = service.ListIncomplete();

            Assert.Equal(2, result.Count);
            Assert.True(result.All(a => !a.IsCompleted));
        }
        [Fact]
        public void Logger_ShouldBeCalledWhenAddingAssignment()
        {
            
            var formatter = new Mock<IAssignmentFormatter>();
            var logger = new Mock<IAppLogger>();
            var service = new AssignmentService(logger.Object, formatter.Object);
            var assignment = new Assignment("test", "more test");

            
            service.AddAssignment(assignment);

            
            logger.Verify(l => l.Log(It.Is<string>(s => s.Contains("Adding assignment"))), Times.Once);
            logger.Verify(l => l.Log(It.Is<string>(s => s.Contains("Assignment added"))), Times.Once);
        }

        [Fact]
        public void Logger_ShouldBeCalledWhenDeletingAssignment()
        {

            var formatter = new Mock<IAssignmentFormatter>();
            var logger = new Mock<IAppLogger>();
            var service = new AssignmentService(logger.Object, formatter.Object);
            var assignment = new Assignment("test", "more test");

            service.AddAssignment(assignment);
            service.DeleteAssignment(assignment.Title);


            logger.Verify(l => l.Log(It.Is<string>(s => s.Contains("Finding assignment for deletion..."))), Times.Once);
            logger.Verify(l => l.Log(It.Is<string>(s => s.Contains("Assignment deleted"))), Times.Once);

        }
        [Fact]
        public void Logger_ShouldBeCalledWhenUpdatingAssignment()
        {

            var formatter = new Mock<IAssignmentFormatter>();
            var logger = new Mock<IAppLogger>();
            var service = new AssignmentService(logger.Object, formatter.Object);
            var assignment = new Assignment("test", "more test");

            var originalTitle = assignment.Title;
            var originalDescription = assignment.Description;
            
            service.AddAssignment(assignment);

            var updateRequest = new UpdateAssignmentRequest("test")
            {
                NewTitle = "new test",
                NewDescription = "test test",
                NewNotes = "Notes"
            };

            service.UpdateAssignment(updateRequest);

            logger.Verify(l => l.Log(It.Is<string>(s => s.Contains(
                "Finding assignment to update..."))), Times.Once);
            logger.Verify(l => l.Log(It.Is<string>(s => s.Contains(
                $"Assignment title: {originalTitle} changed to {updateRequest.NewTitle}"))), Times.Once);
            logger.Verify(l => l.Log(It.Is<string>(s => s.Contains(
                $"Assignment description: {originalDescription} changed to {updateRequest.NewDescription}"))), Times.Once);
            logger.Verify(l => l.Log(It.Is<string>(s => s.Contains(
                $"Notes: {""} changed to {updateRequest.NewNotes}"))), Times.Once);
        }

    }
}
