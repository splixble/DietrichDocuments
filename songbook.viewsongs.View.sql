SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
*   SSMA informational messages:
*   M2SS0003: The following SQL clause was ignored during conversion:
*   ALGORITHM =  UNDEFINED.
*   M2SS0003: The following SQL clause was ignored during conversion:
*   DEFINER = `root`@`localhost`.
*   M2SS0003: The following SQL clause was ignored during conversion:
*   SQL SECURITY DEFINER.
*/
CREATE VIEW [songbook].[viewsongs]
AS
SELECT        songs.TitlePrefix, songs.Title, songs.Code, songs.SongKey, songs.OriginalKey, songs.Comment, songs.PageNumber, songs.intablet, songs.Category, songs.ID, viewsongscovers.Cover, songs.Artist, artists.ArtistFirstName, 
                         artists.ArtistLastName, songs.DiffPDFName, songs.SetlistAddable, LTRIM(ISNULL(songs.TitlePrefix, '') + ' ' + ISNULL(songs.Title, '')) AS FullTitle, LTRIM(ISNULL(artists.ArtistFirstName, '') + ' ' + ISNULL(artists.ArtistLastName, ''))
                          AS FullArtistName, CAST(ISNULL(', :' + songs.Code, '') + ISNULL(', in ' + songs.SongKey, '') + ISNULL(', orig. ' + songs.OriginalKey, '') + ISNULL(', * ' + songs.Comment, '') + ISNULL(', pg ' + songs.PageNumber, '') 
                         AS varchar(8000)) AS SongInfo
FROM            songbook.songs INNER JOIN
                         songbook.viewsongscovers ON songs.ID = viewsongscovers.SongID LEFT OUTER JOIN
                         songbook.artists ON artists.ArtistID = viewsongscovers.ArtistID
GO
