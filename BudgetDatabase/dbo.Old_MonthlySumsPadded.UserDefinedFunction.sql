USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dietrich Neuman
-- Create date: 25-Jan-2025
-- Description:	Returns sums of amounts per Month, Grouping, AccountOwner, and AccountType, with rows added for null amount, to avoid discontinuous lines on the chart.
-- =============================================
CREATE FUNCTION [dbo].[Old_MonthlySumsPadded] 
(
	-- Add the parameters for the function here
	@FromDate Date, 
	@ToDate Date
)
RETURNS TABLE AS

RETURN SELECT AMTS.AmountNormalized, Blanks.GroupingKey, Blanks.TrMonth, Blanks.AccountOwner, Blanks.AccountType FROM 
	(SELECT AmountNormalized, GroupingKey, TrMonth, AccountOwner, AccountType
	  FROM ViewMonthlySums
	  WHERE TrMonth >= @FromDate AND TrMonth <= @ToDate) AS AMTS

RIGHT OUTER JOIN 
( SELECT DISTINCT GRPS.GroupingKey, GRPS.AccountOwner, GRPS.AccountType, MNTHS.MonthDate AS TrMonth
	  FROM ViewMonthlySums AS GRPS
	  CROSS JOIN MonthsInDateRange(@FromDate, @ToDate) AS MNTHS 
	  WHERE TrMonth >= @FromDate AND TrMonth <= @ToDate
	  ) AS Blanks
  ON AMTS.GroupingKey = Blanks.GroupingKey AND AMTS.TrMonth = Blanks.TrMonth AND AMTS.AccountOwner = Blanks.AccountOwner AND AMTS.AccountType = Blanks.AccountType 


GO
