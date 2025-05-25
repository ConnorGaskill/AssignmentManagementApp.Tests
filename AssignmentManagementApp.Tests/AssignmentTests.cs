namespace AssignmentManagementApp.Tests
{
    using AssignmentManagement.Core;
    using AssignmentManagement.Core.Models;

    public class AssignmentTests
    {
        [Fact]
        public void Constructor_ValidInputShouldCreateAssignment()
        {
            var assignment = new Assignment("Read Chapter 2", "Summarize key points", Priority.High, "Notes");
            Assert.Equal("Read Chapter 2", assignment.Title);
            Assert.Equal("Summarize key points", assignment.Description);
            Assert.Equal(Priority.High, assignment.Priority);
            Assert.Equal("Notes", assignment.Notes);
            Assert.False(assignment.IsCompleted);
        }

        [Fact]
        public void Constructor_SetsMediumAsDefaultPriority()
        {
            var assignment = new Assignment("Read Chapter 2", "Summarize key points");

            Assert.Equal(Priority.Medium, assignment.Priority);
        }
        [Fact]
        public void Constructor_SetsEmptyStringAsDefaultNote()
        {
            var assignment = new Assignment("Read Chapter 2", "Summarize key points", Priority.Medium);

            Assert.Equal(assignment.Notes, String.Empty);
        }

        [Fact]
        public void Constructor_AcceptsHighPriority()
        {
            var assignment = new Assignment("Urgent Task", "Do it now", Priority.High);
            Assert.Equal(Priority.High, assignment.Priority);
        }

        [Fact]
        public void Constructor_BlankTitleShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Assignment("", "Valid description"));
        }

        [Fact]
        public void Update_BlankDescriptionShouldThrowException()
        {
            var assignment = new Assignment("Read Chapter 2", "Summarize key points");
            Assert.Throws<ArgumentException>(() => assignment.Update("Valid title", "", assignment.Priority, assignment.Notes));
        }

        [Fact]
        public void MarkComplete_SetsIsCompletedToTrue()
        {
            var assignment = new Assignment("Task", "Complete the lab");
            assignment.MarkComplete();
            Assert.True(assignment.IsCompleted);
        }

    }
}
