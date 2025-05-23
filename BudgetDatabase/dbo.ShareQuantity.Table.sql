USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShareQuantity](
	[SQDate] [date] NOT NULL,
	[SQAccount] [char](4) NOT NULL,
	[NumShares] [decimal](18, 3) NOT NULL,
	[Comment] [varchar](250) NULL,
 CONSTRAINT [PK_FundShares] PRIMARY KEY CLUSTERED 
(
	[SQDate] ASC,
	[SQAccount] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ShareQuantity]  WITH CHECK ADD  CONSTRAINT [FK_ShareQuantity_BudgetAccount] FOREIGN KEY([SQAccount])
REFERENCES [dbo].[Account] ([AccountID])
GO
ALTER TABLE [dbo].[ShareQuantity] CHECK CONSTRAINT [FK_ShareQuantity_BudgetAccount]
GO
