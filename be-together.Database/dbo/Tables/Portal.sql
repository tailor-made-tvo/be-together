/*
    [Status]          0 = Created
	                  1 = Active
				      2 = Locked
    [Template]        1 = Default
	                  1 = ....
*/
CREATE TABLE [dbo].[Portal] (
    [Id]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (128) NOT NULL,
    [PortalTemplateId] BIGINT         NOT NULL CONSTRAINT [FK_Portal_PortalTemplate] REFERENCES dbo.PortalTemplate ([Id]),
    [Status]           INT            CONSTRAINT [DF_Portal_Status]    DEFAULT 0 NOT NULL,
    [CreatedOn]        DATETIME       CONSTRAINT [DF_Portal_CreatedOn] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]        NVARCHAR (50)  CONSTRAINT [DF_Portal_CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [ChangedOn]        DATETIME       CONSTRAINT [DF_Portal_ChangedOn] DEFAULT (getutcdate()) NOT NULL,
    [ChangedBy]        NVARCHAR (50)  CONSTRAINT [DF_Portal_ChangedBy] DEFAULT (suser_sname()) NOT NULL, 
    CONSTRAINT [PK_Portal] PRIMARY KEY ([Id])
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_Portal_Name]
    ON [dbo].[Portal]([Name] ASC) WITH (ALLOW_PAGE_LOCKS = OFF)
    ON [INDEX];
GO

