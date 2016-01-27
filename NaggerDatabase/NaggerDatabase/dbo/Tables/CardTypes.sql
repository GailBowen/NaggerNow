CREATE TABLE [dbo].[frequencys] (
    [ID]          INT          IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (50) NOT NULL,
    [DayCount]    INT          CONSTRAINT [DF_frequencys_DayCount] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_frequencys] PRIMARY KEY CLUSTERED ([ID] ASC)
);

