SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [songbook].[alternateartists](
	[ID] [int] IDENTITY(226,1) NOT NULL,
	[SongID] [int] NOT NULL,
	[ArtistID] [int] NOT NULL,
 CONSTRAINT [PK_alternateartists_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [songbook].[alternateartists]  WITH CHECK ADD  CONSTRAINT [FK_AlternateArtists_ArtistID] FOREIGN KEY([ArtistID])
REFERENCES [songbook].[artists] ([ArtistID])
GO
ALTER TABLE [songbook].[alternateartists] CHECK CONSTRAINT [FK_AlternateArtists_ArtistID]
GO
ALTER TABLE [songbook].[alternateartists]  WITH CHECK ADD  CONSTRAINT [FK_AlternateArtists_SongID] FOREIGN KEY([SongID])
REFERENCES [songbook].[songs] ([ID])
GO
ALTER TABLE [songbook].[alternateartists] CHECK CONSTRAINT [FK_AlternateArtists_SongID]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'songbook.alternateartists' , @level0type=N'SCHEMA',@level0name=N'songbook', @level1type=N'TABLE',@level1name=N'alternateartists'
GO
