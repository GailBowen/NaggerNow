
CREATE PROCEDURE [dbo].[Cards_FetchOptional_Today]
	
AS
BEGIN
	
	SET NOCOUNT ON;

	select 
		cards.ID,
		Title,
		cards.Description,
		boards.description as Board,
		cardtypes.description as CardType,
		token,
		tokensawarded,
		lastdone
		
		from
	  dbo.cards
	  left join dbo.CardTypes on cards.CardType = cardTypes.ID
	  left join dbo.boards on cards.boardid = Boards.id
	  where 
		cards.CardType <> 12
	  and
		convert(date, cards.DueDate) = convert(date, getdate()) 
	  and 
	    (convert(date, lastdone) < convert(date, getdate()) OR lastdone is null)
	   and
	     cards.skippedCount < 2

END