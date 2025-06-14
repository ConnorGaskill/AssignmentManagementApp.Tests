# Assignment Management System

## Overview
This app allows students to track and update tasks using a modular, test-driven architecture.

## Features
- Create, update, and delete tasks
- Console and Web API support
- Moq-based unit testing
- Regression testing and bug tracking

## Setup
1. Clone this repo
2. Open `AssignmentManagement.Tests.sln` in Visual Studio 2022
3. Run tests using Test Explorer

## Architecture
- Assignment Service: Middle layer used to interact with the Assignment object and Manipulate the 
  repo (a List<Assignment> contained inside).
- Assignment Controller: API layer used to format Assignments and related field into HTTP requests. Uses the
  Assignment Service
- Console UI: User interface layer that manages user input and writes to the console. Uses the Assignment 
  Service.

## Contributing
Fork and submit PRs; please include new tests for all changes.

## Maintenance Plan
- Perform quarterly code reviews for complexity and cohesion
- Resolve all GitHub issues within 2 weeks of report
- Document all new services and methods with XML comments
- Expand unit test coverage to at least 80%
