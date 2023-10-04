/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


--CREATE UNIQUE CLUSTERED INDEX PK_UserView_Id
--    ON [dbo].[UserView] (Id)
--	ON [INDEX];
--GO

--CREATE UNIQUE NONCLUSTERED INDEX UK_UserView_LoginId
--    ON [dbo].[UserView] (LoginId)
--	ON [INDEX];
--GO

--CREATE UNIQUE NONCLUSTERED INDEX UK_UserView_LoginName
--    ON [dbo].[UserView] (LoginName)
--	ON [INDEX];
--GO



insert into PortalTemplate (Name, Status) values('Default', 1);
insert into Portal (Name, PortalTemplateId, Status) values('be-together', 1, 1);

EXEC [dbo].[User_Create] @LoginName='tvo', @Password='pwr4tea9', @UserName='Vogt', @UserPreName='Torsten', @DateOfBirth='1970-07-02', @Gender=1

