
CREATE PROCEDURE [dbo].[Cards_FetchSkipped_Today]
	
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
	  left join dbo.skips on cards.id = skips.cardid 
	  where convert(date, skips.Skipped) = convert(date, getdate())
END