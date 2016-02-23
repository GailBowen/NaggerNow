CREATE TABLE [dbo].[CardActions] (
    [ID]            INT  IDENTITY (1, 1) NOT NULL,
    [CardID]        INT  NOT NULL,
    [ActionCreated] DATE CONSTRAINT [DF_CardActions_ActionCreated] DEFAULT (getdate()) NOT NULL,
    [ColumnID]      INT  CONSTRAINT [DF_CardActions_ColumnID] DEFAULT ((1)) NOT NULL,
    [DueDate]       DATE CONSTRAINT [DF_CardActions_DueDate] DEFAULT (getdate()) NULL,
    [LastDone]      DATE NULL,
    [LastSkip]      DATE NULL,
    [SkipCount]     INT  CONSTRAINT [DF_CardActions_Skipped] DEFAULT ((0)) NOT NULL,
    [Completed]     BIT  CONSTRAINT [DF_CardActions_Completed] DEFAULT ((0)) NOT NULL,
    [ActionDone]    BIT  CONSTRAINT [DF_CardActions_ActionDone] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CardActions_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CardActions_Columns] FOREIGN KEY ([ColumnID]) REFERENCES [dbo].[Columns] ([ID])
);

