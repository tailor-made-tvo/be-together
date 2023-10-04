CREATE PROCEDURE [dbo].[User_SetPassword]
    @Id           BIGINT,
    @Password     NVARCHAR (128)
AS
	SET NOCOUNT ON

	DECLARE @tranName NVARCHAR(255)

	SET @tranName = OBJECT_NAME(@@ProcID) + '-' + @Id
	BEGIN TRAN @tranName
	BEGIN TRY
		UPDATE [dbo].[Login]
		SET Password = @Password
		WHERE Id = @Id

		if @@ROWCOUNT < 1 BEGIN
			BEGIN TRY
				THROW 60000, 'UPDATE error: No rows would be updated', 16;
			END TRY
			BEGIN CATCH
				THROW;
			END CATCH
		END if @@ROWCOUNT > 1 BEGIN
			BEGIN TRY
				THROW 60000, 'UPDATE error: Not more than one row at a time could be updated', 16;
			END TRY
			BEGIN CATCH
				THROW;
			END CATCH
		END

		SELECT Id, LoginId, LoginName, NULL as Password, UserName, UserPreName, DateOfBirth, Gender, CreatedOn, CreatedBy, ChangedOn, ChangedBy
		FROM [dbo].[Login]
		WHERE Id = @Id

		COMMIT TRAN @tranName
	END TRY
	BEGIN CATCH
		COMMIT TRAN @tranName;
		THROW
	END CATCH

	RETURN @Id
