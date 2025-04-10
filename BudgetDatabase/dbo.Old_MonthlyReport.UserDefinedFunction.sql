USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Dietrich Neuman
-- Create date: 27-Jan-2025
-- Description:	Returns data for monthly report, shown on both chart and grid.
-- =============================================
CREATE FUNCTION [dbo].[Old_MonthlyReport] 
(
	-- Add the parameters for the function here
	@FromDate Date, 
	@ToDate Date
)
RETURNS TABLE AS

RETURN SELECT TOP 100 PERCENT ISNULL(AmountNormalized, 0) AS AmountNormalized, GroupingKey, TrMonth, AccountOwner, AccountType,
AccountOwner + '-' + AccountType +'-' + GroupingKey + '-' + CONVERT(varchar, TrMonth, 1) AS ViewKey
FROM            MonthlySumsPadded(@FromDate, @ToDate)
ORDER BY TrMonth, GroupingKey

GO
