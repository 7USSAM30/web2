CREATE TABLE [dbo].[orders] (
    [Id]       INT  IDENTITY (1, 1) NOT NULL,
    [custid]   INT  NULL,
    [bookid]   INT  NULL,
    [buydate]  DATE DEFAULT (getdate()) NULL,
    [quantity] INT  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);