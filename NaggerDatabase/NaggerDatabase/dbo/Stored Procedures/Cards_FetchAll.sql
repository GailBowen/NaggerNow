CREATE PROCEDURE [dbo].[Cards_FetchAll]
	
AS
BEGIN
	
	SET NOCOUNT ON;

	select 
		done.lastDone,
		skipped.lastSkip,
		c.*
	from
	cards c
	left join 
	(
		select cardid, max(LogDate) as lastDone from logs where logtype = 'DONE'  group by cardid
	) as done
	on done.CardID = c.id
	left join
	(
		select cardid, max(logdate) as lastSkip from logs where logtype = 'SKIP' group by cardid
	) as skipped
	on skipped.cardid = c.id


END