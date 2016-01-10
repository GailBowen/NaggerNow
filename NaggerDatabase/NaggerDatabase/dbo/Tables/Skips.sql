CREATE TABLE [dbo].[Skips] (
    [CardID]       INT      NOT NULL,
    [Skipped]      DATETIME NOT NULL,
    [ArchivedDate] DATETIME NULL,
    CONSTRAINT [FK_Skips_Cards] FOREIGN KEY ([CardID]) REFERENCES [dbo].[Cards] ([ID])
);

