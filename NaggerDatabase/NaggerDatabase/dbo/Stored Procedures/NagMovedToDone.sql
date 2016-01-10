
CREATE PROCEDURE [dbo].[NagMovedToDone]
	@id int
AS
BEGIN
	
	declare @previousDone datetime

	select @previousDone = lastdone from dbo.Cards where id = @id

	update dbo.Cards set previousDone = @previousDone, LastDone = GETDATE() where id = @id
END