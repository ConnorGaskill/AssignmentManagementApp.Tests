using AssignmentManagement.Core;

using System;

namespace AssignmentManagement.UI
{
    public class ConsoleUI
    {
        private readonly AssignmentService _assignmentService;

        public ConsoleUI(AssignmentService assignmentService)
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
                Console.Write("Choose an option: ");
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
                        MarkAssignmentComplete(); // TODO
                        break;
                    case "5":
                        SearchAssignmentByTitle(); // TODO
                        break;
                    case "6":
                        UpdateAssignment(); // TODO
                        break;
                    case "7":
                        DeleteAssignment(); // TODO
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
            Console.Write("Enter assignment title: ");
            var title = Console.ReadLine();
            Console.Write("Enter assignment description: ");
            var description = Console.ReadLine();

            try
            {
                var assignment = new Assignment(title, description);
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
                Console.WriteLine($"- {assignment.Title}: {assignment.Description} (Completed: {assignment.IsCompleted})");
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
                Console.WriteLine($"- {assignment.Title}: {assignment.Description} (Completed: {assignment.IsCompleted})");
            }
        }

        private void MarkAssignmentComplete()
        {
            // TODO: Implement UI for marking assignment complete
        }

        private void SearchAssignmentByTitle()
        {
            // TODO: Implement UI for searching assignment by title
        }

        private void UpdateAssignment()
        {
            // TODO: Implement UI for updating assignment
        }

        private void DeleteAssignment()
        {
            // TODO: Implement UI for deleting assignment
        }
    }
}
