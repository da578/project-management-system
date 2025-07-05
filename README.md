# Additional

# Project Management System

[![Language - C#](https://img.shields.io/badge/Language-C%23-643cdb)](https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/)
[![Built in - ASP .NET Core Web API](https://img.shields.io/badge/Built_in-ASP_.NET_Core_Web_API-643cdb)](https://dotnet.microsoft.com/en-us/apps/aspnet/apis)
[![ORM - Entity Framework Core](https://img.shields.io/badge/ORM-Entity_Framework_Core-643cdb)](https://learn.microsoft.com/en-us/ef/core/)
[![Arch - Clean Architecture](https://img.shields.io/badge/Arch-Clean_Architecture-4cc9f0)](https://www.geeksforgeeks.org/system-design/complete-guide-to-clean-architecture/)
[![Database - SQL Server](https://img.shields.io/badge/Database-SQL_Server-0b8eec)](https://www.microsoft.com/en-us/sql-server)
[![UNTESTED](https://img.shields.io/badge/UNTESTED-ffdd00)](https://www.microsoft.com/en-us/sql-server)

## Table of Contents

- [Overview](https://www.google.com/search?q=%23overview)
- [Features](https://www.google.com/search?q=%23features)
- [Architecture](https://www.google.com/search?q=%23architecture)
- [Technologies Used](https://www.google.com/search?q=%23technologies-used)
- [Database Design](https://www.google.com/search?q=%23database-design)
- [Getting Started](https://www.google.com/search?q=%23getting-started)
    - [Prerequisites](https://www.google.com/search?q=%23prerequisites)
    - [Installation](https://www.google.com/search?q=%23installation)
    - [Running the Application](https://www.google.com/search?q=%23running-the-application)
- [API Endpoints](https://www.google.com/search?q=%23api-endpoints)
- [Future Enhancements](https://www.google.com/search?q=%23future-enhancements)
- [Contributing](https://www.google.com/search?q=%23contributing)
- [License](https://www.google.com/search?q=%23license)
- [Author](https://www.google.com/search?q=%23author)

## Overview

This Project Management System is a robust backend API designed to streamline project management by providing a clear and efficient structure for managing projects, tasks, and users. Built with **Clean Architecture** principles, it emphasizes separation of concerns, testability, and maintainability, making it ideal for collaborative team environments and scalable enterprise solutions.

The system aims to reduce ambiguity in project workflows, enhance communication, and ensure comprehensive tracking and management of all project aspects.

## Features

The API provides comprehensive functionalities across various modules:

- **User Management:** Register, Login, Update Profile, Delete User, Retrieve User Details (by ID, Username, Email), Retrieve All Users.
- **Project Management:** Create, Update, Delete Project, Retrieve Project Details (by ID), Retrieve All Projects, Retrieve Projects by User.
- **Task Management:** Create, Update, Delete Task, Assign Task, Retrieve Task Details (by ID), Retrieve All Tasks, Retrieve Tasks by Project/Assigned User.
- **Comment Management:** Create, Update, Delete Comment, Retrieve Comment Details (by ID), Retrieve Comments by Task/Project.
- **Attachment Management:** Upload, Update Metadata, Delete Attachment, Retrieve Attachment Details (by ID), Retrieve Attachments by Task/Project.
- **Project Member Management:** Add Member, Update Member Role, Remove Member, Retrieve Member Details (by ID), Retrieve Members by Project/User, Retrieve specific Project Member by Project and User ID.

## Architecture

The system follows a **Clean Architecture** approach, ensuring a highly maintainable, testable, and flexible codebase. It is structured into distinct layers:

- **Domain Layer:** Contains core business entities (`User`, `Project`, `Task`, `Comment`, `Attachment`, `ProjectMember`) and interfaces for repositories (`IUserRepository`, `IProjectRepository`, etc., and `IUnitOfWork`). It is the innermost layer, free from external dependencies.
- **Application Layer:** Defines the application's use cases through **Commands** (for modifications) and **Queries** (for data retrieval). It includes **DTOs** (Data Transfer Objects), **Handlers** (powered by MediatR), and **AutoMapper** for object transformations.
- **Infrastructure Layer:** Provides concrete implementations for interfaces defined in the Domain Layer, such as **Repositories** (using Entity Framework Core) and **DbContext** for database interaction. It also handles Dependency Injection configuration for infrastructure services.
- **API Layer:** The entry point of the application, responsible for handling HTTP requests. It contains **Controllers** that orchestrate Commands and Queries, returning appropriate HTTP responses. Swagger/OpenAPI is integrated for API documentation.

## Technologies Used

- **Programming Language:** C#
- **API Framework:** ASP.NET Core
- **Object-Relational Mapper (ORM):** Entity Framework Core
- **Design Pattern/Library:** MediatR (CQRS and Mediator Pattern)
- **Object Mapping:** AutoMapper
- **Database:** SQL Server
- **API Documentation:** Swagger/OpenAPI (Swashbuckle.AspNetCore)
- **Password Hashing:** BCrypt.Net-Next

## Database Design

The system's database schema is designed to efficiently manage project-related data. Key entities include `User`, `Project`, `Task`, `Comment`, `Attachment`, and `ProjectMember`, with clearly defined relationships (1:Many, XOR relationships) and unique indexes to maintain data integrity. Delete behaviors (Cascade, Restrict, NoAction) are configured for referential integrity.

**To visualize the database schema, please include your Entity-Relationship Diagram (ERD) here.**
You can upload your ERD image to your GitHub repository (e.g., in an `assets` or `docs` folder) and then link it using Markdown:
`![Your ERD Title](path/to/your/erd-image.png)`

### Main Entities and Attributes:

- **User:** `UserID` (PK), `Username` (Unique), `Email` (Unique), `PasswordHash`, `FirstName`, `LastName`, `CreatedAt`, `UpdatedAt`
- **Project:** `ProjectID` (PK), `ProjectName`, `Description`, `StartDate`, `EndDate`, `Status`, `CreatedAt`, `UpdatedAt`, `CreatedByUserID` (FK)
- **Task:** `TaskID` (PK), `ProjectID` (FK), `Title`, `Description`, `AssignedToUserID` (FK, Nullable), `Status`, `Priority`, `DueDate`, `CreatedAt`, `UpdatedAt`
- **Comment:** `CommentID` (PK), `UserID` (FK), `CommentText`, `CreatedAt`, `TaskID` (FK, Nullable - XOR), `ProjectID` (FK, Nullable - XOR)
- **Attachment:** `AttachmentID` (PK), `UserID` (FK), `FileName`, `FilePath`, `FileSizeKB`, `UploadedAt`, `TaskID` (FK, Nullable - XOR), `ProjectID` (FK, Nullable - XOR)
- **ProjectMember:** `ProjectMemberID` (PK), `ProjectID` (FK), `UserID` (FK), `Role`, `JoinedAt`

### Relationships Between Entities:

- **User - Project (CreatedBy):** One `User` can create many `Projects` (1:Many).
- **Project - Task:** One `Project` can have many `Tasks` (1:Many).
- **User - Task (AssignedTo):** One `User` can be assigned to many `Tasks` (1:Many, Nullable).
- **User - Comment:** One `User` can create many `Comments` (1:Many).
- **Comment - Task / Project (XOR):** Each `Comment` is related to either one `Task` OR one `Project`, but not both (XOR Relationship).
- **User - Attachment:** One `User` can upload many `Attachments` (1:Many).
- **Attachment - Task / Project (XOR):** Each `Attachment` is related to either one `Task` OR one `Project`, but not both (XOR Relationship).
- **Project - ProjectMember:** One `Project` can have many `ProjectMembers` (1:Many).
- **User - ProjectMember:** One `User` can be a `ProjectMember` in many `Projects`.

### Database Design Considerations

- **Unique Index:** A unique index on `ProjectMember` (`ProjectID`, `UserID`) ensures that a single user cannot be a member of the same project more than once.
- **Delete Behavior:**
    - `OnDelete(DeleteBehavior.Cascade)` is used for relationships where deleting a primary entity should delete related entities (e.g., deleting a `Project` will delete its associated `ProjectMembers` and `Tasks`).
    - `OnDelete(DeleteBehavior.Restrict)` or `NoAction` is used to prevent the deletion of a primary entity if there are still related entities (e.g., a `User` cannot be deleted if they have created `Projects` or uploaded `Attachments`).
- **Optional Relationships (Nullable Foreign Keys):** `TaskID` and `ProjectID` in `Comment` and `Attachment` are nullable to support XOR relationships, where a comment or attachment can be related to either a task or a project, but not both simultaneously.

## Getting Started

Follow these steps to set up and run the Project Management System API locally.

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server Express/LocalDB)
- A SQL Server Management Studio (SSMS) or Azure Data Studio for database management (optional but recommended).

### Installation

1. **Clone the repository:**
    
    ```
    git clone [https://github.com/your-username/ProjectManagement.git](https://github.com/your-username/ProjectManagement.git) # Replace with your actual repo URL
    cd ProjectManagement
    
    ```
    
2. **Configure Database Connection String:**
Open `appsettings.json` (and `appsettings.Development.json` for development) in the `ProjectManagement.Api` project and update the `DefaultConnection` string to point to your SQL Server instance:
    
    ```
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=YourServerName;Database=ProjectManagementDb;User Id=YourUserId;Password=YourPassword;TrustServerCertificate=True;"
      },
      "Logging": {
        // ...
      }
    }
    
    ```
    
    *Replace `YourServerName`, `ProjectManagementDb`, `YourUserId`, and `YourPassword` with your actual database credentials.*
    
3. **Run Database Migrations:**
Navigate to the `ProjectManagement.Api` project directory in your terminal:
    
    ```
    cd ProjectManagement.Api
    
    ```
    
    Apply the database migrations to create the database schema:
    
    ```
    dotnet ef database update
    
    ```
    
    *If you need to create initial migrations, navigate to `ProjectManagement.Infrastructure` and run `dotnet ef migrations add InitialCreate`, then `dotnet ef database update` from `ProjectManagement.Api`.*
    
4. **Install NuGet Packages:**
Ensure all NuGet packages are restored across the solution. From the solution root:
    
    ```
    dotnet restore
    
    ```
    

### Running the Application

Navigate to the `ProjectManagement.Api` project directory and run the application:

```
cd ProjectManagement.Api
dotnet run

```

The API will typically run on `http://localhost:5000` (HTTP) and `https://localhost:5001` (HTTPS) by default, or as configured in `launchSettings.json`.

## API Endpoints

Once the application is running, you can explore the API endpoints using Swagger UI:

- Open your web browser and navigate to: `https://localhost:5001/swagger` (replace port if different).

Swagger UI provides an interactive interface to view available endpoints, their expected request formats, and example responses.

## Future Enhancements

- **Advanced Authentication & Authorization:** Full JWT implementation and Role-Based Access Control (RBAC).
- **Real-time Notifications:** Integration with SignalR or WebSockets for instant updates.
- **Sophisticated File Management:** Integration with cloud storage (Azure Blob Storage, AWS S3).
- **Advanced Collaboration Features:** Internal chat, Kanban boards, shared calendars.
- **Third-Party Integrations:** Connect with Jira, Trello, Slack, Microsoft Teams.
- **UI/UX Development:** Build a responsive frontend using React, Angular, or Vue.js.
- **Logging & Monitoring:** Centralized logging (ELK Stack, Azure Monitor) and APM tools.
- **Improved Validation:** Implement FluentValidation for robust input validation.

## Contributing

Contributions are welcome! If you have suggestions, bug reports, or want to contribute code, please feel free to open an issue or submit a pull request.

## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT). See the `LICENSE` file in the repository for details.