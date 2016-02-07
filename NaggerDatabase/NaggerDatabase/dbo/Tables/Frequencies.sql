CREATE TABLE [dbo].[Frequencies] (
    [Description] VARCHAR (50) NOT NULL,
    [DayCount]    INT          CONSTRAINT [DF_Frequencies_DayCount] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Frequencies_1] PRIMARY KEY CLUSTERED ([DayCount] ASC)
);



