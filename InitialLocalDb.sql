CREATE TABLE [dbo].[USERS]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1), 
    [Password] NCHAR(255) NULL, 
    [Username] NCHAR(255) NULL, 
    [CreationDate] DATETIME NULL, 
    [CurrentSessionToken] NCHAR(255) NULL, 
    [SessionStart] DATETIME NULL,
	[IsActive] BIT NOT NULL,
	[DateOfBlock] DATETIME NULL
)
-------------------------------------------
CREATE TABLE [dbo].Students
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1), 
    [FirstName] NCHAR(255) NULL, 
    [Surname] NCHAR(255) NULL, 
    [Mail] NCHAR(255) NULL, 
    [User_Id] INT NULL, 
    CONSTRAINT [FK_Students_ToUser] FOREIGN KEY (User_Id) REFERENCES Users(Id)
)
-------------------------------------------
CREATE TABLE [dbo].Subjects
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1), 
    [Name] NCHAR(255) NULL, 
    [Degree] INT NOT NULL DEFAULT 0,
    [Schedule] NCHAR(255) NULL, 
    [Date] DATETIME NULL
)
-------------------------------------------
CREATE TABLE [dbo].StudentSubjects
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1), 
    [Student_Id] INT NULL, 
    [Subject_Id] INT NULL, 
    [Registered] BIT NULL, 
    CONSTRAINT [FK_StudentSubject_ToStudent] FOREIGN KEY (Student_Id) REFERENCES Students(Id), 
    CONSTRAINT [FK_StudentSubject_ToSubject] FOREIGN KEY (Subject_Id) REFERENCES Subjects(Id)
)
-------------------------------------------

INSERT INTO USERS (CreationDate, "PASSWORD", "USERNAME", SessionStart, CurrentSessionToken, IsActive) VALUES 
('05/06/2018', '123456', 'MyUser', NULL, NULL, 1);
INSERT INTO STUDENTS ("USER_ID", "SURNAME", "FIRSTNAME", MAIL) VALUES (1, 'SURNAME', 'FIRST_NAME', 'MyMAIL');
INSERT INTO SUBJECTS (DEGREE, "NAME", "SCHEDULE", Date) VALUES (3, 'NAME', '19-21HS','05/06/2018');