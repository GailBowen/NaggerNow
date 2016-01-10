CREATE TABLE [dbo].[Boards] (
    [ID]          INT          IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Board] PRIMARY KEY CLUSTERED ([ID] ASC)
);

