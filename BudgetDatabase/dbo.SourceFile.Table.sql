USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SourceFile](
	[FileID] [int] IDENTITY(1,1) NOT NULL,
	[FilePath] [varchar](250) NULL,
	[Account] [char](4) NULL,
	[ImportDateTime] [datetime2](7) NULL,
	[ManuallyEntered] [bit] NOT NULL,
	[StatementDate] [date] NULL,
 CONSTRAINT [PK_BudgetSourceFile] PRIMARY KEY CLUSTERED 
(
	[FileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SourceFile] ADD  CONSTRAINT [DF_BudgetSourceFile_ManuallyEntered]  DEFAULT ((0)) FOR [ManuallyEntered]
GO
