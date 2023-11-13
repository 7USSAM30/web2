CREATE TABLE [dbo].[items] (
    [Id]          INT        IDENTITY (1, 1)    NOT NULL,
    [name]        VARCHAR (50)  NULL,
    [description] VARCHAR (MAX) NULL,
    [price]       INT           NULL,
    [quantity]    INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

