using AssignmentManagement.Core.DTOs;
using AssignmentManagement.Core.Interfaces;
using AssignmentManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssignmentManagement.Core.Services
{
    /// <summary>
    /// Object representing the Assignment Service used as a middle 
    /// layer to communicate with the Assignment class.
    /// 
    /// Responsible for repository manipulation, logging, and request handling.
    /// 
    /// Requires IAppLogger and IAssignmentFormatter to be injected.
    /// </summary>
    public class AssignmentService : IAssignmentService
    {
        private readonly List<Assignment> _assignments = new();

        private readonly IAppLogger _logger;

        private readonly IAssignmentFormatter _formatter;

        public AssignmentService(IAppLogger logger, IAssignmentFormatter formatter)
        {
            _logger = logger;
            _formatter = formatter;
        }
        /// <summary>
        /// Adds an assignment to _assignments
        /// The title of the assignment being added must be unique
        /// Checks if the assignment title already exists
        /// </summary>
        /// <param name="assignment">The assignment being added</param>
        /// <returns>
        /// True if the assignment was added successfully
        /// False if the assignment could not be added (duplicate title)
        /// </returns>
        public bool AddAssignment(Assignment assignment)
        {
            _logger.Log("Adding assignment...");
            if (_assignments.Any(a => a.Title.Equals(assignment.Title, StringComparison.OrdinalIgnoreCase)))
            {
                _logger.Log($"Assignment already exists:\n{_formatter.Format(assignment)}\n");
                return false;
            }

            _assignments.Add(assignment);
            _logger.Log("Assignment added");
            return true;
        }
        /// <summary>
        /// Gets _assignments
        /// </summary>
        /// <returns>A list containing all assignments</returns>
        public List<Assignment> ListAll()
        {
            _logger.Log("Listing assignments...");
            return _assignments;
        }
        /// <summary>
        /// Gets a list of all incomplete assignments
        /// </summary>
        /// <returns>A list of incomplete assignments</returns>
        /// <exception cref="ArgumentException">If _assignments is null or empty</exception>
        public List<Assignment> ListIncomplete()
        {
            _logger.Log("Listing incomplete assignments...");
            List<Assignment> incomplete = new();

            if (_assignments.Count == 0 || _assignments == null)
                throw new ArgumentException("No assignments");

            return _assignments.Where(a => !a.IsCompleted).ToList();
        }
        /// <summary>
        /// Gets an assignment based on a title
        /// </summary>
        /// <param name="title">The title of the assignment</param>
        /// <returns>
        /// An assignment matching a provided title
        /// Null if there are no matching assignments
        /// </returns>
        public Assignment? FindAssignmentByTitle(string title)
        {
            return _assignments.FirstOrDefault(a => a.Title.Equals(
                title, StringComparison.OrdinalIgnoreCase));
        }
        /// <summary>
        /// Gets an assignment based on an ID
        /// </summary>
        /// <param name="id">The ID</param>
        /// <returns>
        /// An assignment matching a provided ID
        /// </returns>
        public Assignment? FindAssignmentById(Guid id)
        {
            return _assignments.FirstOrDefault(a => a.Id == id);
        }
        /// <summary>
        /// Marks an assignment complete based on a title
        /// </summary>
        /// <param name="title">The title of the assignment</param>
        /// <returns>
        /// True if the assignment was successfully marked complete
        /// False if the assignment was not successfully marked complete (no matching title)
        /// </returns>
        public bool MarkAssignmentComplete(string title)        
        {
            var assignment = FindAssignmentByTitle(title);

            if (assignment == null)
                return false;
            assignment.MarkComplete();
            return true;
        }
        /// <summary>
        /// Deletes an assignment from _assignments based on a title
        /// </summary>
        /// <param name="title">The title of the assignment</param>
        /// <returns>
        /// True if the assignment was successfully deleted
        /// False if the assignment was not successfully deleted (no mathcing title)
        /// </returns>
        public bool DeleteAssignment(string title)
        {
            _logger.Log("Finding assignment for deletion...");
            var assignment = FindAssignmentByTitle(title);
            if (assignment == null)
            {
                _logger.Log($"Assignment \"{title}\" not found");
                return false;
            }

            _assignments.Remove(assignment);
            _logger.Log("Assignment deleted");
            return true;
        }
        /// <summary>
        /// Updates an assignment
        /// Uses ValidateRequest to check if the DTO contains a field with data that can
        /// overwrite a matching field in the assignment being updated
        /// </summary>
        /// <param name="request">
        /// A DTO containing the title of the assignment being updated and fields
        /// matching that of the assignment class. If any fields in the DTO
        /// are not null, their data will overwrite the data in matching fields
        /// in the target assignment
        /// </param>
        /// <returns>
        /// True if an assignment with a title matching the "OldTitle" field in the request was found
        /// False if an assignment with a title matching the "OldTitle" field in the request was not found
        /// </returns>
        public bool UpdateAssignment(UpdateAssignmentRequest request)
        {
            _logger.Log("Finding assignment to update...");
            Assignment? assignment = FindAssignmentByTitle(request.OldTitle);
            if (assignment == null) {

                _logger.Log("Assignment not found");
                return false;
            }

            if (request.NewTitle != null &&
                !request.OldTitle.Equals(request.NewTitle, StringComparison.OrdinalIgnoreCase) &&
                _assignments.Any(a => a.Title.Equals(request.NewTitle, StringComparison.OrdinalIgnoreCase)))
            {
                _logger.Log($"There is already an Assignment with title {request.NewTitle}");
                return false;
            }

            string newTitle = ValidateRequest(request.NewTitle, assignment.Title);

            string newDescription = ValidateRequest(request.NewDescription, assignment.Description);

            Priority newPriority = request.NewPriority ?? assignment.Priority;
            if (request.NewPriority.HasValue)
                _logger.Log($"Assignment priority: {assignment.Priority} changed to {request.NewPriority}");

            string newNotes = ValidateRequest(request.NewNotes, assignment.Notes);

            assignment.Update(newTitle, newDescription, newPriority, newNotes);

            return true;
        }
        /// <summary>
        /// Checks string fields in an assignment being updated against string fields in
        /// a DTO.
        /// </summary>
        /// <param name="request">A field of the DTO matching the assignment being updated</param>
        /// <param name="assignmentDefault">The filed of the assignment being updated</param>
        /// <returns>
        /// A string containing the data within the field of the assignment if request is null
        /// A string containing the data within the field of the request if request is not null
        /// </returns>
        public string ValidateRequest(string request, string assignmentDefault)
        {
            string newValue = request ?? assignmentDefault;

            if (!newValue.Equals(assignmentDefault, StringComparison.OrdinalIgnoreCase))
                _logger.Log($"{assignmentDefault} changed to {request}");

            return newValue;
        }
        /// <summary>
        /// Calls the formatter to format a priority to a string
        /// </summary>
        /// <param name="priority">A priority</param>
        /// <returns>A string representing a priority</returns>
        public string FormatPriorityToString(Priority priority) {

            return _formatter.FormatPriorityToString(priority);
        
        }
        /// <summary>
        /// Calls the formatter to format a string into a priority
        /// </summary>
        /// <param name="priority">The string</param>
        /// <returns>
        /// A priority representing a the string
        /// Null if the string was not able to be converted
        /// </returns>
        public Priority? FormatStringToPriority(string priority) { 
        
            return _formatter.FormatStringToPriority(priority);
        
        }

    }


}
