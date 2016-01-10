CREATE TABLE [dbo].[CardTypes] (
    [ID]          INT          IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (50) NOT NULL,
    [DayCount]    INT          CONSTRAINT [DF_CardTypes_DayCount] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CardTypes] PRIMARY KEY CLUSTERED ([ID] ASC)
);

