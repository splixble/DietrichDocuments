SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [songbook].[venues](
	[ID] [int] IDENTITY(72,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Comment] [varchar](20) NULL,
 CONSTRAINT [PK_venues_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [songbook].[venues] ADD  DEFAULT (NULL) FOR [Comment]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'songbook.venues' , @level0type=N'SCHEMA',@level0name=N'songbook', @level1type=N'TABLE',@level1name=N'venues'
GO
