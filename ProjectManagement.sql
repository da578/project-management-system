CREATE DATABASE ProjectManagement;
GO

USE ProjectManagement;
GO

CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE Projects (
    ProjectID INT IDENTITY(1,1) PRIMARY KEY,
    ProjectName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    StartDate DATE,
    EndDate DATE,
    Status NVARCHAR(20) DEFAULT 'Not Started',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    CreatedByUserID INT NOT NULL,
    FOREIGN KEY (CreatedByUserID) REFERENCES Users(UserID)
    CONSTRAINT CK_ProjectStatus CHECK (Status IN ('Not Started', 'In Progress', 'Completed', 'On Hold', 'Cancelled'))
);
GO

CREATE TABLE ProjectMembers (
    ProjectMemberID INT IDENTITY(1,1) PRIMARY KEY,
    ProjectID INT NOT NULL,
    UserID INT NOT NULL,
    Role NVARCHAR(50) DEFAULT 'Member',
    JoinedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID) ON DELETE CASCADE,
    FOREGIN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    UNIQUE (ProjectID, UserID)
    CONSTRAINT CK_MemberRole CHECK (Role IN ('Admin', 'Manager', 'Member', 'Viewer'))
);
GO

CREATE TABLE Tasks (
    TaskID INT IDENTITY(1,1) PRIMARY KEY,
    ProjectID INT NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    AssignedToUserID INT,
    Status NVARCHAR(20) DEFAULT 'To Do',
    Priority NVARCHAR(20) DEFAULT 'Medium',
    DueDate DATE,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID) ON DELETE CASCADE,
    FOREIGN KEY (AssignedToUserID) REFERENCES Users(UserID)
    CONSTRAINT CK_TaskStatus CHECK (Status IN ('To Do', 'In Progress', 'Done', 'Blocked', 'Review'))
    CONSTRAINT CK_TaskPriority CHECK (Priority IN ('Low', 'Medium', 'High', 'Urgent'))
);

CREATE TABLE Comments (
    CommentID INT IDENTITY(1,1) PRIMARY KEY,
    TaskID INT,
    ProjectID INT,
    UserID INT NOT NULL,
    CommentText NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TaskID) REFERENCES Tasks(TaskID) ON DELETE CASCADE,
    FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID) ON DELETE CASCADE,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT CK_Comment_OneEntity CHECK (
        (TaskID IS NOT NULL AND ProjectID IS NULL) OR
        (TaskID IS NULL AND ProjectID IS NOT NULL)
    )
);
GO

CREATE TABLE Attachments (
    AttachmentID INT IDENTITY(1,1) PRIMARY KEY,
    TaskID INT
    ProjectID INT
    UserID INT NOT NULL,
    FileName NVARCHAR(255) NOT NULL,
    FilePath NVARCHAR(MAX) NOT NULL,
    FileSizeKB INT
    UploadedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TaskID) REFERENCES Tasks(TaskID) ON DELETE CASCADE,
    FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID) ON DELETE CASCADE,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT CK_Attachment_OneEntity CHECK (
        (TaskID IS NOT NULL AND ProjectID IS NULL) OR
        (TaskID IS NULL AND ProjectID IS NOT NULL)
    )
);
GO