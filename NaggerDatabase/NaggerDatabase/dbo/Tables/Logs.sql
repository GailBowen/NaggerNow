CREATE TABLE [dbo].[Logs] (
    [ID]      INT        IDENTITY (1, 1) NOT NULL,
    [CardID]  INT        NOT NULL,
    [LogType] NCHAR (10) CONSTRAINT [DF_Logs_Type] DEFAULT (N'DONE') NOT NULL,
    [LogDate] DATE       NOT NULL,
    CONSTRAINT [PK_TaDas] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_TaDas_Cards] FOREIGN KEY ([CardID]) REFERENCES [dbo].[Cards] ([ID])
);



