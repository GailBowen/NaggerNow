CREATE TABLE [dbo].[Skips] (
    [ID]     INT  IDENTITY (1, 1) NOT NULL,
    [CardID] INT  NOT NULL,
    [Done]   DATE NOT NULL,
    CONSTRAINT [PK_Skips] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Skips_Cards] FOREIGN KEY ([CardID]) REFERENCES [dbo].[Cards] ([ID])
);



