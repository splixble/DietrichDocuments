SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetTypePattern](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Pattern] [varchar](max) NOT NULL,
	[TrType] [char](4) NULL,
	[ForIgnore] [bit] NOT NULL,
	[ForIncome] [bit] NOT NULL,
 CONSTRAINT [PK_BudgetTypePattern] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[BudgetTypePattern] ADD  CONSTRAINT [DF_BudgetTypePattern_ForIgnore]  DEFAULT ((0)) FOR [ForIgnore]
GO
ALTER TABLE [dbo].[BudgetTypePattern]  WITH CHECK ADD  CONSTRAINT [FK_BudgetTypePattern_BudgetTypeGroupings] FOREIGN KEY([TrType])
REFERENCES [dbo].[BudgetTypeGroupings] ([TRTypeID])
GO
ALTER TABLE [dbo].[BudgetTypePattern] CHECK CONSTRAINT [FK_BudgetTypePattern_BudgetTypeGroupings]
GO
