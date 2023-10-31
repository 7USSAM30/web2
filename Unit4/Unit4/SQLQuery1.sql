CREATE TABLE [dbo].[book] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [title]        VARCHAR (50) NULL,
    [info]         VARCHAR (50) NULL,
    [price]        INT          NULL,
    [discount]     VARCHAR (50) NULL,
    [pubdate]      DATE         NULL,
    [category]     INT          NULL,
    [bookquantity] INT          NULL,
    [imgfile]      VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
