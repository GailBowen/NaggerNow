
CREATE PROCEDURE [dbo].[Card_Update]
	@id int,
	@columnID INT,
	@description varchar(max),
	@dueDate date,
	@lastDone date,
	@lastSkip date,
	@skipCount int

AS
BEGIN
	
	update 
		dbo.Cards 
	set 
		Description = @description
	where 
		id = @id


		INSERT INTO [dbo].[CardActions]
           ([CardID]
           ,[ActionCreated]
           ,[ColumnID]
           ,[DueDate]
           ,[LastDone]
           ,[LastSkip]
           ,[SkipCount])
     VALUES
           (@id
           ,getdate()
           ,@columnID
           ,@dueDate
           ,@lastDone
           ,@lastSkip
           ,@skipCount)
           

END