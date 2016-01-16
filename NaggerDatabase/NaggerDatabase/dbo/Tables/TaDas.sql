CREATE TABLE [dbo].[TaDas] (
    [CardID] INT  NOT NULL,
    [Done]   DATE NOT NULL,
    CONSTRAINT [FK_TaDas_Cards] FOREIGN KEY ([CardID]) REFERENCES [dbo].[Cards] ([ID])
);

