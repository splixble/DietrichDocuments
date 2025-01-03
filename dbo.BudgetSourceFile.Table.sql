SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetSourceFile](
	[FileID] [int] IDENTITY(1,1) NOT NULL,
	[FilePath] [varchar](250) NULL,
	[Account] [char](4) NULL,
	[ImportDateTime] [datetime2](7) NULL,
	[ManuallyEntered] [bit] NOT NULL,
	[StatementDate] [date] NULL,
 CONSTRAINT [PK_BudgetSourceFile] PRIMARY KEY CLUSTERED 
(
	[FileID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BudgetSourceFile] ADD  CONSTRAINT [DF_BudgetSourceFile_ManuallyEntered]  DEFAULT ((0)) FOR [ManuallyEntered]
GO
