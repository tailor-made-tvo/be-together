CREATE UNIQUE CLUSTERED INDEX PK_UserView_Id
    ON [dbo].[UserView] (Id)
	ON [INDEX];
GO

CREATE UNIQUE NONCLUSTERED INDEX UK_UserView_LoginId
    ON [dbo].[UserView] (LoginId)
	ON [INDEX];
GO

CREATE UNIQUE NONCLUSTERED INDEX UK_UserView_LoginName
    ON [dbo].[UserView] (LoginName)
	ON [INDEX];
GO
