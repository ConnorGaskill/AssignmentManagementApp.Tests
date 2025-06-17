using Microsoft.VisualBasic;
using System.Text.Json.Serialization;

namespace AssignmentManagement.Core.Models
{
    /// <summary>
    /// An object representing an Assignment containing a title (required), 
    /// description (required), completion status (default: false),
    /// ID (generated), priority (default: medium), and notes (optional).
    /// 
    /// This is the model for the API.
    /// 
    /// Responsible for simulating an assignment and modifying its contents.
    /// 
    /// Must be accessed by the Assignment Service.
    /// </summary>
    public class Assignment
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool IsCompleted { get; private set; } = false;
        public Guid Id { get; } = new Guid();
        public Priority Priority { get; private set; }
        public string Notes {  get; private set; }

        public Assignment(string title, string description, Priority priority = Priority.Medium, string notes = "")
        {
            Validate(title, nameof(title));
            Validate(description, nameof(description));

            Title = title;
            Description = description;
            Priority = priority;
            Notes = notes;
        }
        /// <summary>
        /// Method for updating an assignment
        /// Uses Validate to check is the title or description is null or empty
        /// </summary>
        /// <param name="newTitle">The new title</param>
        /// <param name="newDescription">The new description</param>
        /// <param name="priority">The new priority</param>
        /// <param name="newNotes">The new notes</param>
        public void Update(string newTitle, string newDescription, Priority priority, string newNotes)
        {
            Validate(newTitle, nameof(newTitle));
            Validate(newDescription, nameof(newDescription));

            Priority = priority;
            Title = newTitle;
            Description = newDescription;
            Notes = newNotes;
        }

        /// <summary>
        /// Marks the assignment complete
        /// </summary>
        public void MarkComplete()
        {
            IsCompleted = true;
        }
        /// <summary>
        /// Checks if params are null or whitespace
        /// </summary>
        /// <param name="input">The string content</param>
        /// <param name="fieldName">The name of the field being checked</param>
        /// <exception cref="ArgumentException">Thrown if given values are null or whitespace</exception>
        private void Validate(string input, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException($"{fieldName} cannot be blank or whitespace.");
        }
    }
}
