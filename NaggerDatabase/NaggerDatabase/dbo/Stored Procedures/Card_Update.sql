
CREATE PROCEDURE [dbo].[Card_Update]
	@id int,
	@columnID INT,
	@description varchar(max),
	@dueDate date,
	@skipCount int
AS
BEGIN
	
	update 
		dbo.Cards 
	set 
		ColumnID = @columnID,
		Description = @description,
		DueDate = @dueDate,
		SkipCount = @skipCount
	where 
		id = @id

END