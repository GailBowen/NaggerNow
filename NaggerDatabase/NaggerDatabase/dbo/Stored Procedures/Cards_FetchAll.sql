
CREATE PROCEDURE [dbo].[Cards_FetchAll]
	
AS
BEGIN
	
	SET NOCOUNT ON;

	select 
		*
		from
	  dbo.cards
	  left join dbo.CardTypes on cards.CardType = cardTypes.ID
	  left join dbo.boards on cards.boardid = Boards.id
	  where Archived = 0 
	 

END