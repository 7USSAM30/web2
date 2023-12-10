
CREATE TABLE [dbo].[orders] (

    [id]        INT  IDENTITY (1, 1) NOT NULL,

    [bookId]    INT  NOT NULL,

    [userid]    INT  NULL,

    [quantity]  INT  NULL,

    [orderdate] DATE DEFAULT (getdate()) NULL,

    CONSTRAINT [PK_orders] PRIMARY KEY CLUSTERED ([id] ASC)

);


CREATE TABLE [dbo].[usersaccounts] (

    [Id]   INT          IDENTITY (1, 1) NOT NULL,

    [name] VARCHAR (50) NULL,

    [pass] VARCHAR (50) NULL,

    [role] VARCHAR (50) NULL,

    PRIMARY KEY CLUSTERED ([Id] ASC)

);