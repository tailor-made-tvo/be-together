/*
	@type    0 = ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789
	         1 = ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz
	         2 = ???
*/
CREATE PROCEDURE [dbo].[CreateSecureKey]
    @key     varchar(256) OUTPUT,
    @type    INT = 0,
	@len     SMALLINT = 16
AS
    SET NOCOUNT ON
    
	if @len < 1 BEGIN
        BEGIN TRY
            THROW 60000, 'Key length must be between 1 and 255', 16;
        END TRY
        BEGIN CATCH
            THROW;
        END CATCH
    END

    DECLARE @keys     VARCHAR(128)
    DECLARE @i        int

	if @type = 0 BEGIN
		SET @keys = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789'
	END ELSE if @type = 1 BEGIN
		SET @keys = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz'
	END ELSE BEGIN
        BEGIN TRY
            THROW 60000, 'Type is not supported', 16;
        END TRY
        BEGIN CATCH
            THROW;
        END CATCH
	END

    SET @i = 0
    SET @key = ''

    WHILE @i < @len BEGIN
        SET @key = @key + SUBSTRING(@keys, CONVERT(INT, RAND() * LEN(@keys) + 1), 1)
        SET @i = @i + 1
    END

    RETURN len(@key)
