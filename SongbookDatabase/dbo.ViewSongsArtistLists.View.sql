SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[ViewSongsArtistLists]
AS
SELECT        MAX(ID) AS ID, MAX(Title) AS Title, 
STRING_AGG(FullArtistName, '|') WITHIN GROUP ( ORDER BY ID ASC, Cover, Artist ) AS ArtistsWithVirgules
FROM            songbook.viewsongs
GROUP BY ID
GO
