-- select * from dbo.cards
CREATE PROCEDURE [dbo].[Cards_FetchAll]
	
AS
BEGIN
	
	SET NOCOUNT ON;

	select 
		done.lastDone,
		skip.lastSkip,
		c.*
	from
	cards c
	left join 
	(
		select cardid, max(LogDate) as lastDone from logs where type = 'DONE'  group by cardid
	) as done
	on done.CardID = c.id
	left join
	(
		select cardid, max(logdate) as lastSkip from logs where type = 'SKIP' group by cardid
	) as skip
	on skip.cardid = c.id


END