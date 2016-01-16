CREATE TABLE [dbo].[TaDas] (
    [ID]     INT  IDENTITY (1, 1) NOT NULL,
    [CardID] INT  NOT NULL,
    [Done]   DATE NOT NULL,
    CONSTRAINT [PK_TaDas] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_TaDas_Cards] FOREIGN KEY ([CardID]) REFERENCES [dbo].[Cards] ([ID])
);



