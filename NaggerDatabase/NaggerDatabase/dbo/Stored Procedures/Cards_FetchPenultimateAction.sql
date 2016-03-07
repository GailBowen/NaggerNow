-- [dbo].[Cards_FetchPenultimateAction] 2
CREATE PROCEDURE [dbo].[Cards_FetchPenultimateAction]
	@cardID int
AS
BEGIN
	
	SELECT a.*, c.*
	FROM dbo.CardActions a
	left join dbo.Cards c on c.id = a.CardID
	where cardid = @cardID
	ORDER BY a.id DESC
	OFFSET 1 ROW
	FETCH FIRST 1 ROW ONLY 
	
END