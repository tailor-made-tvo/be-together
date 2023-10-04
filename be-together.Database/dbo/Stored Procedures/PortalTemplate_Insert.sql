CREATE PROCEDURE [dbo].[PortalTemplate_Insert]
    @PortalTemplateName    NVARCHAR (128),
    @UserName     NVARCHAR (128),
    @UserPreName  NVARCHAR (128),
    @DateOfBirth  DATE,
    @Gender       INT
AS
	SET NOCOUNT ON
	
	--DECLARE @keys     VARCHAR(128)
	--DECLARE @i        int
	--DECLARE @id       BIGINT
	--DECLARE @PortalTemplateId  VARCHAR(16)
	--DECLARE @tranName NVARCHAR(255)
	--SET @keys = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789'
	--SET @i = 0
	--SET @PortalTemplateId = ''

	--WHILE @i < 16 BEGIN
	--	SET @PortalTemplateId = @PortalTemplateId + SUBSTRING(@keys, CONVERT(INT, RAND() * LEN(@keys) + 1), 1)
	--	SET @i = @i + 1
	--END

	--if @PortalTemplateName is NULL
	--	SET @PortalTemplateName = @PortalTemplateId

	--SET @tranName = OBJECT_NAME(@@ProcID) + '-' + @PortalTemplateId + '-' + @PortalTemplateName
	--BEGIN TRAN @tranName
	--BEGIN TRY
	--	INSERT [dbo].[PortalTemplate] (PortalTemplateId, PortalTemplateName, UserName, UserPreName, DateOfBirth, Gender)
	--	VALUES (@PortalTemplateId, @PortalTemplateName, @UserName, @UserPreName, @DateOfBirth, @Gender)
	--	SET @id = SCOPE_IDENTITY()

	--	if @@ROWCOUNT < 1 BEGIN
	--		BEGIN TRY
	--			THROW 60000, 'INSERT error: No rows would be added', 16;
	--		END TRY
	--		BEGIN CATCH
	--			THROW;
	--		END CATCH
	--	END if @@ROWCOUNT > 1 BEGIN
	--		BEGIN TRY
	--			THROW 60000, 'INSERT error: Not more than one row at a time could be added', 16;
	--		END TRY
	--		BEGIN CATCH
	--			THROW;
	--		END CATCH
	--	END

	--	SELECT Id, PortalTemplateId, PortalTemplateName, UserName, UserPreName, DateOfBirth, Gender, CreatedOn, CreatedBy, ChangedOn, ChangedBy
	--	FROM [dbo].[PortalTemplate]
	--	WHERE Id = @Id

	--	COMMIT TRAN @tranName
	--END TRY
	--BEGIN CATCH
	--	ROLLBACK TRAN @tranName;
	--	THROW
	--END CATCH

	--RETURN @id
