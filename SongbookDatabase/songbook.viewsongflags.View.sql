SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* 
*   SSMA error messages:
*   M2SS0201: MySQL standard function GROUP_CONCAT is not supported in current SSMA version

/*
*   SSMA informational messages:
*   M2SS0003: The following SQL clause was ignored during conversion:
*   ALGORITHM =  UNDEFINED.
*   M2SS0003: The following SQL clause was ignored during conversion:
*   DEFINER = `root`@`localhost`.
*   M2SS0003: The following SQL clause was ignored during conversion:
*   SQL SECURITY DEFINER.
*/ */

CREATE VIEW [songbook].[viewsongflags] ([Song], [Flags])
AS 
   SELECT max(flaggedsongs.Song) AS Song, STRING_AGG(songbook.flags.FlagCode, ', ') AS Flags
   FROM (songbook.flaggedsongs 
      INNER JOIN songbook.flags 
      ON ((flaggedsongs.FlagID = flags.FlagID)))
   GROUP BY flaggedsongs.Song
GO
