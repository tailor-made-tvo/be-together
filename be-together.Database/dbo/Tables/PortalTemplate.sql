/*
    [Status]          0 = Created
	                  1 = Active
				      2 = Locked
*/
CREATE TABLE [dbo].[PortalTemplate] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (128) NOT NULL,
    [Status]      INT            CONSTRAINT [DF_PortalTemplate_Status]    DEFAULT 0 NOT NULL,
    [CreatedOn]   DATETIME       CONSTRAINT [DF_PortalTemplate_CreatedOn] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]   NVARCHAR (50)  CONSTRAINT [DF_PortalTemplate_CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [ChangedOn]   DATETIME       CONSTRAINT [DF_PortalTemplate_ChangedOn] DEFAULT (getutcdate()) NOT NULL,
    [ChangedBy]   NVARCHAR (50)  CONSTRAINT [DF_PortalTemplate_ChangedBy] DEFAULT (suser_sname()) NOT NULL, 
    CONSTRAINT [PK_PortalTemplate] PRIMARY KEY ([Id])
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_PortalTemplate_Name]
    ON [dbo].[PortalTemplate]([Name] ASC) WITH (ALLOW_PAGE_LOCKS = OFF)
    ON [INDEX];
GO