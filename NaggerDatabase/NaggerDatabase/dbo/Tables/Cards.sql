CREATE TABLE [dbo].[Cards] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [BoardID]     INT           CONSTRAINT [DF_Cards_ListID] DEFAULT ((1)) NOT NULL,
    [FrequencyID] INT           NOT NULL,
    [Mandated]    BIT           CONSTRAINT [DF_Cards_Mandated] DEFAULT ((0)) NOT NULL,
    [LocationID]  INT           CONSTRAINT [DF_Cards_LocationID] DEFAULT ((1)) NOT NULL,
    [Title]       VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (MAX) NULL,
    [Created]     DATE          CONSTRAINT [DF_Cards_Created] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Cards] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Cards_Boards] FOREIGN KEY ([BoardID]) REFERENCES [dbo].[Boards] ([ID]),
    CONSTRAINT [FK_Cards_Frequencies] FOREIGN KEY ([FrequencyID]) REFERENCES [dbo].[Frequencies] ([DayCount]),
    CONSTRAINT [FK_Cards_Locations] FOREIGN KEY ([LocationID]) REFERENCES [dbo].[Locations] ([ID])
);













