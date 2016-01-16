-- select * from dbo.cards
CREATE PROCEDURE [dbo].[Cards_FetchAll]
	
AS
BEGIN
	
	SET NOCOUNT ON;

	
	SELECT 
		t1.done as lastdone,
		s1.done as lastskipped,
		c.*
	FROM cards c
	   left outer JOIN tadas t1
	  ON (c.id = t1.cardid)
	 LEFT OUTER JOIN tadas t2
		ON (c.id = t2.cardid AND t1.id < t2.id)
	  left outer JOIN skips s1
	  ON (c.id = s1.cardid)
	 LEFT OUTER JOIN tadas s2
		ON (c.id = s2.cardid AND s1.id < s2.id)
	 where t2.id is null and s2.id is null
	      and
		  Archived = 0

END