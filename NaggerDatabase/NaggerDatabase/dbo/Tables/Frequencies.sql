CREATE TABLE [dbo].[Frequencies] (
    [ID]          INT          IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (50) NOT NULL,
    [DayCount]    INT          CONSTRAINT [DF_Frequencies_DayCount] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Frequencies] PRIMARY KEY CLUSTERED ([ID] ASC)
);

