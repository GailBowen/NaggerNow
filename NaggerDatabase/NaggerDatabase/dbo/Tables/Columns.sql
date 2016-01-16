CREATE TABLE [dbo].[Columns] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NCHAR (10)    NULL,
    [Description] NVARCHAR (50) NULL,
    CONSTRAINT [PK_Columns] PRIMARY KEY CLUSTERED ([ID] ASC)
);

