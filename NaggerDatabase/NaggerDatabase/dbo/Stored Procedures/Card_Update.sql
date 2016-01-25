
CREATE PROCEDURE [dbo].[Card_Update]
	@id int,
	@columnID INT,
	@description varchar(max)
AS
BEGIN
	
	update 
		dbo.Cards 
	set 
		ColumnID = @columnID,
		Description = @description
	where 
		id = @id

END