-- [dbo].[Cards_FetchPenultimateAction] 2
create PROCEDURE [dbo].[Cards_FetchCurrentAction]
	@cardID int
AS
BEGIN
	
	SELECT a.*, c.*
	FROM dbo.CardActions a
	left join dbo.Cards c on c.id = a.CardID
	where cardid = @cardID
	ORDER BY a.id DESC
	OFFSET 0 ROW
	FETCH FIRST 1 ROW ONLY 
	
END