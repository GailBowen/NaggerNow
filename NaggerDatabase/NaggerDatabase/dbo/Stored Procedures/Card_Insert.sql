
--select * from dbo.boards
--select * from dbo.frequencies
--select * from dbo.locations
-- dbo.Card_Insert 8,7,1,2,'Cook weekly','Hugh Recipes?'
CREATE PROCEDURE dbo.Card_Insert
		   @BoardID int,
           @FrequencyID int,
           @Mandated bit,
           @LocationID int,
           @Title varchar(50),
           @Description varchar(max)
AS
BEGIN
	
	SET NOCOUNT ON;

    
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
END