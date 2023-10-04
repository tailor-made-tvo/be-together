/*
    [Gender]          0 = Unknown
                      1 = Man
                      2 = Woman
                      3 = Group
    [Status]          0 = Created
	                  1 = Active
				      2 = Locked
*/
CREATE TABLE [dbo].[Login] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [LoginId]     CHAR (16)      NOT NULL,
    [LoginName]   NVARCHAR (128) NOT NULL,
    [Password]    NVARCHAR (128) NULL,
    [UserName]    NVARCHAR (128) NULL,
    [UserPreName] NVARCHAR (128) NULL,
    [DateOfBirth] DATE           NULL,
    [Gender]      INT            CONSTRAINT [DF_Login_Gender]    DEFAULT 0 NOT NULL,
    [Status]      INT            CONSTRAINT [DF_Login_Status]    DEFAULT 0 NOT NULL,
    [CreatedOn]   DATETIME       CONSTRAINT [DF_Login_CreatedOn] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]   NVARCHAR (50)  CONSTRAINT [DF_Login_CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [ChangedOn]   DATETIME       CONSTRAINT [DF_Login_ChangedOn] DEFAULT (getutcdate()) NOT NULL,
    [ChangedBy]   NVARCHAR (50)  CONSTRAINT [DF_Login_ChangedBy] DEFAULT (suser_sname()) NOT NULL, 
    CONSTRAINT [PK_Login] PRIMARY KEY ([Id])
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_Login_LoginId]
    ON [dbo].[Login]([LoginId] ASC) WITH (ALLOW_PAGE_LOCKS = OFF)
    ON [INDEX];
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_Login_LoginName]
    ON [dbo].[Login]([LoginName] ASC) WITH (ALLOW_PAGE_LOCKS = OFF)
    ON [INDEX];
GO
