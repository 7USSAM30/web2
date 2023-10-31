CREATE TABLE [dbo].[usersaccounts] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [name] VARCHAR (50) NULL,
    [pass] VARCHAR (50) NULL,
    [role] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
