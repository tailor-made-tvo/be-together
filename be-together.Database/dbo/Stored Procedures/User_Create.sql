CREATE PROCEDURE [dbo].[User_Create]
    @LoginName    NVARCHAR (128),
    @Password     NVARCHAR (128),
    @UserName     NVARCHAR (128),
    @UserPreName  NVARCHAR (128),
    @DateOfBirth  DATE,
    @Gender       INT
AS
    SET NOCOUNT ON
    
    DECLARE @id       BIGINT
	DECLARE @loginId  CHAR(16)
	EXEC [dbo].[CreateSecureKey] @loginId out, 0, 16

    DECLARE @tranName NVARCHAR(255)
    SET @tranName = OBJECT_NAME(@@ProcID) + '-' + @loginId

    BEGIN TRAN @tranName
    BEGIN TRY
		if @LoginName is NULL
			SET @LoginName = @loginId

        INSERT [dbo].[Login] (LoginId, LoginName, Password, UserName, UserPreName, DateOfBirth, Gender)
        VALUES (@loginId, @loginName, @Password, @UserName, @UserPreName, @DateOfBirth, @Gender)
        SET @id = SCOPE_IDENTITY()

        if @@ROWCOUNT < 1 BEGIN
            BEGIN TRY
                THROW 60000, 'INSERT error: No rows would be added', 16;
            END TRY
            BEGIN CATCH
                THROW;
            END CATCH
        END if @@ROWCOUNT > 1 BEGIN
            BEGIN TRY
                THROW 60000, 'INSERT error: Not more than one row at a time could be added', 16;
            END TRY
            BEGIN CATCH
                THROW;
            END CATCH
        END

        SELECT Id, LoginId, LoginName, NULL as Password, UserName, UserPreName, DateOfBirth, Gender, Status
        FROM [dbo].[Login]
        WHERE Id = @Id

        COMMIT TRAN @tranName
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN @tranName;
        THROW
    END CATCH

    RETURN @id
