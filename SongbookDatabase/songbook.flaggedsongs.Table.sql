SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [songbook].[flaggedsongs](
	[FlaggedSongID] [int] IDENTITY(944,1) NOT NULL,
	[Song] [int] NOT NULL,
	[FlagID] [int] NOT NULL,
 CONSTRAINT [PK_flaggedsongs_FlaggedSongID] PRIMARY KEY CLUSTERED 
(
	[FlaggedSongID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'songbook.flaggedsongs' , @level0type=N'SCHEMA',@level0name=N'songbook', @level1type=N'TABLE',@level1name=N'flaggedsongs'
GO
