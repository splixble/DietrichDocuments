SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [songbook].[songperformances](
	[ID] [int] IDENTITY(4162,1) NOT NULL,
	[Performance] [int] NOT NULL,
	[Song] [int] NOT NULL,
	[Comment] [varchar](250) NULL,
	[SetNumber] [tinyint] NULL,
 CONSTRAINT [PK_songperformances_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [Song] ON [songbook].[songperformances]
(
	[Song] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [songbook].[songperformances] ADD  DEFAULT (NULL) FOR [Comment]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'songbook.songperformances' , @level0type=N'SCHEMA',@level0name=N'songbook', @level1type=N'TABLE',@level1name=N'songperformances'
GO
