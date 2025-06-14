﻿using AssignmentManagement.Core.DTOs;
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

        public List<Assignment> ListAll()
        {
            _logger.Log("Listing assignments...");
            return _assignments;
        }

        public List<Assignment> ListIncomplete()
        {
            _logger.Log("Listing incomplete assignments...");
            List<Assignment> incomplete = new();

            if (_assignments.Count == 0 || _assignments == null)
                throw new ArgumentException("No assignments");

            return _assignments.Where(a => !a.IsCompleted).ToList();
        }
        public Assignment? FindAssignmentByTitle(string title)
        {
            return _assignments.FirstOrDefault(a => a.Title.Equals(
                title, StringComparison.OrdinalIgnoreCase));
        }

        public Assignment? FindAssignmentById(Guid id)
        {
            return _assignments.FirstOrDefault(a => a.Id == id);
        }
        public bool MarkAssignmentComplete(string title)        
        {
            var assignment = FindAssignmentByTitle(title);

            if (assignment == null)
                return false;
            assignment.MarkComplete();
            return true;
        }
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

        public string ValidateRequest(string request, string assignmentDefault)
        {
            string newValue = request ?? assignmentDefault;

            if (!newValue.Equals(assignmentDefault, StringComparison.OrdinalIgnoreCase))
                _logger.Log($"{assignmentDefault} changed to {request}");

            return newValue;
        }

        public string FormatPriorityToString(Priority priority) {

            return _formatter.FormatPriorityToString(priority);
        
        }

        public Priority? FormatStringToPriority(string priority) { 
        
            return _formatter.FormatStringToPriority(priority);
        
        }

    }


}
