
CREATE PROCEDURE [dbo].[NagMovedToDone]
	@id int
AS
BEGIN
	
	declare @previousDone datetime
	declare @dayCount int

	select 
		@previousDone = lastdone,
		@dayCount = dayCount
	from dbo.Cards
	left join dbo.CardTypes on cards.CardType = cardtypes.id
	where cards.id = @id

	update dbo.Cards set previousDone = @previousDone, LastDone = GETDATE(), dueDate = getdate() + @dayCount where id = @id

END