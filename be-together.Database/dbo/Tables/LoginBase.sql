CREATE TABLE [dbo].[LoginBase] (
    [LoginId]   CHAR (16)      NOT NULL,
    [LoginName] NVARCHAR (128) NOT NULL,
    [Locked]    DATETIME       NULL,
    [CreatedOn] DATETIME       CONSTRAINT [DF_LoginBase_CreatedOn] DEFAULT (getutcdate()) NULL,
    [CreatedBy] NVARCHAR (50)  CONSTRAINT [DF_LoginBase_CreatedBy] DEFAULT (suser_sname()) NULL,
    [ChangedOn] DATETIME       CONSTRAINT [DF_LoginBase_ChangedOn] DEFAULT (getutcdate()) NULL,
    [ChangedBy] NVARCHAR (50)  CONSTRAINT [DF_LoginBase_ChangedBy] DEFAULT (suser_sname()) NULL,
    CONSTRAINT [PK_LoginBase] PRIMARY KEY CLUSTERED ([LoginId] ASC) WITH (ALLOW_PAGE_LOCKS = OFF) ON [INDEX]
) on [PRIMARY];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IDX_LoginName]
    ON [dbo].[LoginBase]([LoginName] ASC) WITH (ALLOW_PAGE_LOCKS = OFF)
    ON [INDEX];

