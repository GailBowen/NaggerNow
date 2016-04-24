--select * from dbo.columns
--select * from dbo.boards
--select * from dbo.frequencies
--select * from dbo.locations
-- dbo.Card_Insert 1,8,7,1,2,'Cook weekly','Hugh Recipes?'
CREATE PROCEDURE [dbo].[Card_Insert]
		   @ColumnID int,
		   @BoardID int,
           @FrequencyID int,
           @Mandated bit,
           @LocationID int,
           @Title varchar(50),
           @Description varchar(max)
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @CardID int

    
	INSERT INTO [dbo].[Cards]
           ([BoardID]
           ,[FrequencyID]
           ,[Mandated]
           ,[LocationID]
           ,[Title]
           ,[Description]
           ,[Created])
     VALUES
           (@BoardID
           ,@FrequencyID
           ,@Mandated
           ,@LocationID
           ,@Title
           ,@Description
           ,getdate())

     SELECT @CardID = @@IDENTITY

	 PRINT @CardID


	  INSERT INTO [dbo].[CardActions]
           ([CardID]
           ,[ColumnID]
           ,[DueDate])
     VALUES
           (@CardID
           ,@ColumnID
           ,GETDATE() + (@FrequencyID -1))
           
END