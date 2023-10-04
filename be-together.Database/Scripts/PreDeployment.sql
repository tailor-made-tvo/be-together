/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/



declare @sql varchar(500)
declare @name varchar(200)
declare @xtype varchar(128)

/*
	Löschen aller Views, Procedures und Functions
*/
declare cur cursor for
select [name], xtype from sysobjects where xtype in ('V', 'P', 'F')

open cur
while 1 = 1 begin
	fetch next from cur into @name, @xtype
	if @@fetch_status <> 0 break

	if @xtype like 'V' BEGIN
		set @sql = 'drop view ' + @name
	END ELSE if @xtype like 'P' BEGIN
		set @sql = 'drop procedure ' + @name
	END ELSE if @xtype like 'F' BEGIN
		set @sql = 'drop function ' + @name
	END ELSE BEGIN
		continue
	END

	exec(@sql)
end
close cur
deallocate cur


/*
	Löschen aller Tabellen
*/
EXEC sp_MSforeachtable @command1 = "DROP table ?"
