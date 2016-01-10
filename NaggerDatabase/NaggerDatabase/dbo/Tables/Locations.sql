CREATE TABLE [dbo].[Locations] (
    [ID]          INT          IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED ([ID] ASC)
);

