using AssignmentManagement.Core;
using AssignmentManagement.Core.DTOs;
using AssignmentManagement.Core.Interfaces;
using AssignmentManagement.Core.Models;
using System;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace AssignmentManagement.UI
{
    public class ConsoleUI
    {
        private readonly IAssignmentService _assignmentService;

        public ConsoleUI(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\nAssignment Manager Menu:");
                Console.WriteLine("1. Add Assignment");
                Console.WriteLine("2. List All Assignments");
                Console.WriteLine("3. List Incomplete Assignments");
                Console.WriteLine("4. Mark Assignment as Complete");
                Console.WriteLine("5. Search Assignment by Title");
                Console.WriteLine("6. Update Assignment");
                Console.WriteLine("7. Delete Assignment");
                Console.WriteLine("0. Exit");
                Console.WriteLine("Choose an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddAssignment();
                        break;
                    case "2":
                        ListAllAssignments();
                        break;
                    case "3":
                        ListIncompleteAssignments();
                        break;
                    case "4":
                        MarkAssignmentComplete();
                        break;
                    case "5":
                        SearchAssignmentByTitle();
                        break;
                    case "6":
                        UpdateAssignment();
                        break;
                    case "7":
                        DeleteAssignment();
                        break;
                    case "0":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        private void AddAssignment()
        {
            Console.WriteLine("Enter assignment title: ");
            var title = Console.ReadLine();
            Console.WriteLine("Enter assignment description: ");
            var description = Console.ReadLine();

            Priority priority = GetUserPriority() ?? Priority.Medium;

            Console.WriteLine("(Optional) Enter your notes");
            string notes = Console.ReadLine();
            
            try
            {
                var assignment = new Assignment(title, description, priority, notes);

                if (_assignmentService.AddAssignment(assignment))
                {
                    Console.WriteLine("Assignment added successfully.");
                }
                else
                {
                    Console.WriteLine("An assignment with this title already exists.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void ListAllAssignments()
        {
            var assignments = _assignmentService.ListAll();
            if (assignments.Count == 0)
            {
                Console.WriteLine("No assignments found.");
                return;
            }

            foreach (var assignment in assignments)
            {
                Console.WriteLine($"-(Priority: {assignment.Priority}) {assignment.Title}: {assignment.Description} (Completed: {assignment.IsCompleted}) \n" +
                    $" Notes: {assignment.Notes}");
            }
        }

        private void ListIncompleteAssignments()
        {
            var assignments = _assignmentService.ListIncomplete();
            if (assignments.Count == 0)
            {
                Console.WriteLine("No incomplete assignments found.");
                return;
            }

            foreach (var assignment in assignments)
            {
                Console.WriteLine($"-(Priority: {assignment.Priority}) {assignment.Title}: {assignment.Description} (Incomplete: {assignment.IsCompleted}) \n" +
                    $" Notes: {assignment.Notes}");
            }
        }

        private void MarkAssignmentComplete()
        {
            Console.Write("Enter the title of the assignment to mark complete: ");
            var title = Console.ReadLine();
            if (_assignmentService.MarkAssignmentComplete(title))
            {
                Console.WriteLine("Assignment marked as complete.");
            }
            else
            {
                Console.WriteLine("Assignment not found.");
            }
        }

        private void SearchAssignmentByTitle()
        {
            Console.Write("Enter the title to search: ");
            var title = Console.ReadLine();
            var assignment = _assignmentService.FindAssignmentByTitle(title);

            if (assignment == null)
            {
                Console.WriteLine("Assignment not found.");
            }
            else
            {
                Console.WriteLine($"Found: (Priority: {assignment.Priority}) {assignment.Title}: {assignment.Description} (Completed: {assignment.IsCompleted}) \n" +
                    $" Notes: {assignment.Notes}");
            }
        }

        private void UpdateAssignment()
        {
            string? userInput;

            Console.WriteLine("Enter the current title of the assignment you wish to update or press ENTER to quit: ");

            userInput = Console.ReadLine();

            if (String.IsNullOrEmpty(userInput))
                return;

            UpdateAssignmentRequest request = new UpdateAssignmentRequest(userInput);

            Console.WriteLine("Enter a new title or press ENTER to skip: ");

            userInput = Console.ReadLine();

            if (!String.IsNullOrEmpty(userInput))
                request.NewTitle = userInput;

            Console.WriteLine("Enter a new description or press ENTER to skip: ");

            userInput = Console.ReadLine();

            if (!String.IsNullOrEmpty(userInput))
                request.NewDescription = userInput;

            request.NewPriority = GetUserPriority();

            string? notes = null;

            Console.WriteLine("Enter your notes or press ENTER to skip: ");

            userInput = Console.ReadLine();

            notes = userInput;

            if (!String.IsNullOrEmpty(userInput))
                request.NewNotes = notes;

            _assignmentService.UpdateAssignment(request);
            return;
        }
        private void DeleteAssignment()
        {
            Console.Write("Enter the title of the assignment to delete: ");
            var title = Console.ReadLine();
            if (_assignmentService.DeleteAssignment(title))
            {
                Console.WriteLine("Assignment deleted successfully.");
            }
            else
            {
                Console.WriteLine("Assignment not found.");
            }
        }

        private Priority? GetUserPriority()
        {
            string userInput;

            while (true)
            {
                Console.WriteLine("Enter a new priority level:\n" +
                        "(H)igh\n" +
                        "(M)edium\n" +
                        "(L)ow\n" +
                        "Or press ENTER to skip: ");

                userInput = Console.ReadLine();

                if (!String.IsNullOrEmpty(userInput))
                {
                    Priority? priority = _assignmentService.FormatStringToPriority(userInput);

                    if (priority == null)
                    {
                        Console.WriteLine("Invalid Priority");
                    }
                    else
                    {
                        return priority;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
