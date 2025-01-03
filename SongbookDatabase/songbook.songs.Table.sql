SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [songbook].[songs](
	[TitlePrefix] [varchar](50) NULL,
	[Title] [varchar](max) NOT NULL,
	[Code] [varchar](50) NULL,
	[Comment] [varchar](max) NULL,
	[PageNumberOriginal] [int] NULL,
	[Category] [varchar](50) NULL,
	[ID] [int] IDENTITY(1959,1) NOT NULL,
	[Artist] [int] NULL,
	[SongKey] [varchar](50) NULL,
	[SongbookOnly] [varchar](1) NULL,
	[PageNumber] [varchar](50) NULL,
	[OriginalKey] [varchar](50) NULL,
	[intablet] [bit] NOT NULL,
	[DiffPDFName] [nvarchar](max) NULL,
	[SetlistAddable] [bit] NOT NULL,
 CONSTRAINT [PK_songs_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [songbook].[songs] ADD  DEFAULT (NULL) FOR [PageNumberOriginal]
GO
ALTER TABLE [songbook].[songs] ADD  DEFAULT (NULL) FOR [Artist]
GO
ALTER TABLE [songbook].[songs] ADD  DEFAULT (NULL) FOR [SongbookOnly]
GO
ALTER TABLE [songbook].[songs] ADD  DEFAULT (NULL) FOR [PageNumber]
GO
ALTER TABLE [songbook].[songs] ADD  DEFAULT (0x00) FOR [intablet]
GO
ALTER TABLE [songbook].[songs] ADD  DEFAULT (0x00) FOR [SetlistAddable]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'songbook.songs' , @level0type=N'SCHEMA',@level0name=N'songbook', @level1type=N'TABLE',@level1name=N'songs'
GO
