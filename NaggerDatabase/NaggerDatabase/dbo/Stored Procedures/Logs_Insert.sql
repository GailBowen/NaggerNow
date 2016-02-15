-- select * from dbo.logs
-- [dbo].[Logs_Insert] 18, 'DONE', '2016-01-07'
CREATE PROCEDURE [dbo].[Logs_Insert]
	@CardID int,
	@Type nchar(10),
	@LogDate DATE
AS
BEGIN
	
	INSERT INTO [dbo].[Logs]
           ([CardID]
           ,[Type]
           ,[LogDate])
     VALUES
           (@CardID
           ,@Type
           ,@LogDate)

END