CREATE TABLE [dbo].[Cards] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [BoardID]       INT            CONSTRAINT [DF_Cards_ListID] DEFAULT ((1)) NOT NULL,
    [CardType]      INT            NOT NULL,
    [LocationID]    INT            CONSTRAINT [DF_Cards_LocationID] DEFAULT ((1)) NOT NULL,
    [Title]         VARCHAR (50)   NOT NULL,
    [Description]   VARCHAR (MAX)  NULL,
    [Created]       DATETIME       CONSTRAINT [DF_Cards_Created] DEFAULT (getdate()) NOT NULL,
    [Token]         DECIMAL (5, 2) CONSTRAINT [DF_Cards_Token] DEFAULT ((0)) NOT NULL,
    [TokensAwarded] DECIMAL (5, 2) CONSTRAINT [DF_Cards_TokensAwarded] DEFAULT ((0)) NOT NULL,
    [Archived]      BIT            CONSTRAINT [DF_Cards_Completed] DEFAULT ((0)) NOT NULL,
    [DueDate]       DATETIME       NULL,
    [SkippedCount]  INT            CONSTRAINT [DF_Cards_Skipped] DEFAULT ((0)) NOT NULL,
    [LastDone]      DATETIME       NULL,
    [PreviousDone]  DATETIME       NULL,
    [Url]           VARCHAR (500)  NULL,
    [Url2]          VARCHAR (500)  NULL,
    CONSTRAINT [PK_Cards] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Cards_Boards] FOREIGN KEY ([BoardID]) REFERENCES [dbo].[Boards] ([ID]),
    CONSTRAINT [FK_Cards_CardTypes] FOREIGN KEY ([CardType]) REFERENCES [dbo].[CardTypes] ([ID]),
    CONSTRAINT [FK_Cards_Locations] FOREIGN KEY ([LocationID]) REFERENCES [dbo].[Locations] ([ID])
);

