namespace AssignmentManagement.Tests;

using Xunit;
using AssignmentManagement.UI;
using AssignmentManagement.Core;
using Moq;

public class ConsoleUITests
{
    [Fact]
    public void AddAssignment_ShouldCallAddAssignment()
    {
        var mockService = new Mock<IAssignmentService>();
        var assignment = new Assignment("Read Chapter 2", "Summarize key points");

        ConsoleUI ui = new ConsoleUI(mockService.Object);

        mockService.Object.AddAssignment(assignment);

        mockService.Verify(s => s.AddAssignment(It.Is<Assignment>(
        a => a.Title == "Read Chapter 2" && a.Description 
        == "Summarize key points")), Times.Once);
    }

    [Fact]
    public void DeleteAssignment_ShouldCallDeleteAssignment()
    {
        var mockService = new Mock<IAssignmentService>();
        var consoleUI = new ConsoleUI(mockService.Object);

        var titleToDelete = "Read Chapter 2";

        mockService.Object.DeleteAssignment(titleToDelete);

        mockService.Verify(service => service.DeleteAssignment(titleToDelete), Times.Once);
    }

    [Fact]
    public void ListAllAssignments_ReturnsAssignments()
    {
        // Arrange
        var mockService = new Mock<IAssignmentService>();
        mockService.Setup(service => service.ListAll()).Returns(new List<Assignment>
            {
                new Assignment("Sample1", "Desc1"),
                new Assignment("Sample2", "Desc2")
            });

        var consoleUI = new ConsoleUI(mockService.Object);

        // Act
        var assignments = mockService.Object.ListAll();

        // Assert
        Assert.Equal(2, assignments.Count);
    }

    [Fact]
    public void SearchAssignmentByTitle_ShouldReturnCorrectAssignment()
    {
        var mockService = new Mock<IAssignmentService>();
        var expected = new Assignment("Read Chapter 2", "Summarize key points");

        mockService.Setup(s => s.FindAssignmentByTitle("Read Chapter 2")).Returns(expected);

        var ui = new ConsoleUI(mockService.Object);

        var result = mockService.Object.FindAssignmentByTitle("Read Chapter 2");

        Assert.Equal(expected.Title, result.Title);
        Assert.Equal(expected.Description, result.Description);
        mockService.Verify(s => s.FindAssignmentByTitle("Read Chapter 2"), Times.Once);
    }
}