SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [songbook].[performances](
	[ID] [int] IDENTITY(856,1) NOT NULL,
	[PerformanceDate] [date] NOT NULL,
	[Venue] [int] NOT NULL,
	[Comment] [varchar](250) NULL,
	[Series] [int] NULL,
	[PerformanceType] [char](1) NULL,
	[DidILead] [bit] NOT NULL,
 CONSTRAINT [PK_performances_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [songbook].[performances] ADD  DEFAULT (NULL) FOR [Comment]
GO
ALTER TABLE [songbook].[performances] ADD  DEFAULT (NULL) FOR [Series]
GO
ALTER TABLE [songbook].[performances] ADD  DEFAULT (NULL) FOR [PerformanceType]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'songbook.performances' , @level0type=N'SCHEMA',@level0name=N'songbook', @level1type=N'TABLE',@level1name=N'performances'
GO
