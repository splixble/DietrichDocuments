SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [songbook].[artists](
	[ArtistFirstName] [varchar](max) NULL,
	[ArtistLastName] [varchar](max) NULL,
	[ArtistID] [int] IDENTITY(592,1) NOT NULL,
 CONSTRAINT [PK_artists_ArtistID] PRIMARY KEY CLUSTERED 
(
	[ArtistID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'songbook.artists' , @level0type=N'SCHEMA',@level0name=N'songbook', @level1type=N'TABLE',@level1name=N'artists'
GO
