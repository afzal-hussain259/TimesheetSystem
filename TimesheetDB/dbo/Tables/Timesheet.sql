CREATE TABLE [dbo].[Timesheet]
(
	[Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [UserName]           NVARCHAR (50)  NOT NULL,
    [Date]               DATE           NOT NULL,
    [Project]            NVARCHAR (50)  NOT NULL,
    [DescriptionOfTasks] NVARCHAR (MAX) NOT NULL,
    [HoursWorked]        DECIMAL (18)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
