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

        public void Update(string newTitle, string newDescription, Priority priority, string newNotes)
        {
            Validate(newTitle, nameof(newTitle));
            Validate(newDescription, nameof(newDescription));

            Priority = priority;
            Title = newTitle;
            Description = newDescription;
            Notes = newNotes;
        }

        public void MarkComplete()
        {
            IsCompleted = true;
        }
        private void Validate(string input, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException($"{fieldName} cannot be blank or whitespace.");
        }
    }
}
