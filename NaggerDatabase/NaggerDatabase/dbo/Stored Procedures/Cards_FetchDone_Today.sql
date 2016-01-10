
Create PROCEDURE [dbo].[Cards_FetchDone_Today]
	
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
	  
	  where convert(date, LastDone) = convert(date, getdate())

END