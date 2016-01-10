
CREATE PROCEDURE [dbo].[NagMovedToOptional]
	@id int
AS
BEGIN
	
	declare @skippedCount int
	declare @previousDone datetime
	
	select 
		@skippedCount = skippedCount,
		@previousDone = previousDone
	from 
	dbo.Cards

	if exists (select 1 from Skips where cardid = @id and convert(date, skipped) = convert(date, getdate()))
	BEGIN
		set @skippedCount = @skippedCount - 1
	
		delete from Skips where cardid = @id and convert(date, skipped) = convert(date, getdate())

		update dbo.Cards set  DueDate = getdate(), skippedCount = @skippedCount where id = @id
	END
	ELSE
	BEGIN
		update dbo.Cards set  DueDate = getdate(), LastDone = @previousDone where id = @id
	END
	
	
END