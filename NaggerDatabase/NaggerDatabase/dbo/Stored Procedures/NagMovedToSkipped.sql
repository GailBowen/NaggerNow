
CREATE PROCEDURE [dbo].[NagMovedToSkipped]
	@id int
AS
BEGIN
	
	declare @skippedCount int

	declare @DaysToAdd int

	select 
		@skippedCount = skippedCount,
		@DaysToAdd = daycount
	from 
	dbo.Cards
	left join dbo.CardTypes on Cards.CardType = cardtypes.ID
	where Cards.id = @id

	set @skippedCount = @skippedCount + 1

	update dbo.Cards set skippedCount = @skippedCount, DueDate = getdate() + @DaysToAdd where id = @id


	INSERT INTO [dbo].[Skips]
           ([CardID]
           ,[Skipped])
     VALUES
           (@id
           ,getdate())
           

END