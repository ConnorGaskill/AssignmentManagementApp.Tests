namespace AssignmentManagementApp.Tests
{
    using Xunit;
    using AssignmentManagement.Core;

    public class AssignmentServiceTests
    {
        [Fact]
        public void ListIncomplete_ShouldReturnOnlyAssignmentsThatAreNotCompleted()
        {
            // Arrange
            var service = new AssignmentService();
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
            var service = new AssignmentService();

            // Calling ListIncomplete without any assignments
            Assert.Throws<ArgumentException>(() => service.ListIncomplete());
        }
        [Fact]
        public void ListIncomplete_ShouldReturnAllAssignmentsThatAreNotCompleted()
        {
            var service = new AssignmentService();
            var a1 = new Assignment("Incomplete Task", "Do something");
            var a2 = new Assignment("Completed Task", "Do something else");
            var a3 = new Assignment("Incomplete Task", "Do something cool");
            a2.MarkComplete();

            service.AddAssignment(a1);
            service.AddAssignment(a2);
            service.AddAssignment(a3);

            var result = service.ListIncomplete();

            Assert.Equal(2, result.Count);
            Assert.True(result.All(a => !a.IsCompleted));
        }
    }
}
