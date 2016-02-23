CREATE PROCEDURE [dbo].[Cards_FetchAll]
	
AS
BEGIN
	
	SET NOCOUNT ON;

	--http://stackoverflow.com/questions/2111384/sql-join-selecting-the-last-records-in-a-one-to-many-relationship

	select 
		c.*, acts1.*
	from
	cards c
	left join CardActions acts1 on (c.id = acts1.CardID)
	left outer join CardActions acts2 on (c.id = acts2.CardID and
		(acts1.ActionCreated < acts2.ActionCreated or acts1.ActionCreated = acts2.ActionCreated and acts1.CardID < acts2.CardID))
    where acts2.CardID is null

	
	

END