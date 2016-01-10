
CREATE PROCEDURE [dbo].[NagMovedToMandated]
	@id int
AS
BEGIN
	
	declare @previousDone datetime

	select @previousDone = previousDone from dbo.Cards where id = @id

	update dbo.Cards set LastDone = @previousDone where id = @id
END