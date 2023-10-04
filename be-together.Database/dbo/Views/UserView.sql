CREATE VIEW [dbo].[UserView]
    WITH SCHEMABINDING
	AS  SELECT Id, LoginId, LoginName, Password, UserName, UserPreName, DateOfBirth, Gender, Status
        FROM [dbo].[Login]
GO
