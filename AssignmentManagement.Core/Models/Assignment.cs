using System.Text.Json.Serialization;

namespace AssignmentManagement.Core.Models
{
    public class Assignment
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool IsCompleted { get; private set; } = false;
        public Guid Id { get; } = new Guid();
        public Priority Priority { get; private set; }

        public Assignment(string title, string description, Priority priority = Priority.Medium)
        {
            Validate(title, nameof(title));
            Validate(description, nameof(description));

            Title = title;
            Description = description;
            Priority = priority;
        }

        public void Update(string newTitle, string newDescription, Priority priority)
        {
            Validate(newTitle, nameof(newTitle));
            Validate(newDescription, nameof(newDescription));

            Priority = priority;
            Title = newTitle;
            Description = newDescription;
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
