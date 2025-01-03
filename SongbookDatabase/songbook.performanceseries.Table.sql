SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [songbook].[performanceseries](
	[ID] [int] IDENTITY(3,1) NOT NULL,
	[SeriesName] [varchar](50) NOT NULL,
	[StartDate] [date] NOT NULL,
	[VenueIfAny] [int] NULL,
	[Comment] [varchar](250) NULL,
 CONSTRAINT [PK_performanceseries_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [songbook].[performanceseries] ADD  DEFAULT (NULL) FOR [VenueIfAny]
GO
ALTER TABLE [songbook].[performanceseries] ADD  DEFAULT (NULL) FOR [Comment]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'songbook.performanceseries' , @level0type=N'SCHEMA',@level0name=N'songbook', @level1type=N'TABLE',@level1name=N'performanceseries'
GO
