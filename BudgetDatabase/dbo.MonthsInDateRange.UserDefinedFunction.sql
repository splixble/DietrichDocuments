USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dietrich Neuman
-- Create date: 5-June-2024
-- Description:	Returns records containing every first day of the month between parameters @BeginDate and @EndDate.

-- NOTE: Bug here -- it goes one month past the current date.
-- =============================================
CREATE FUNCTION [dbo].[MonthsInDateRange] 
(	
	-- Add the parameters for the function here
	@BeginDate Date, 
	@EndDate Date
)
RETURNS TABLE 
AS RETURN
	WITH SubQuery AS 
	(SELECT DATEFROMPARTS(YEAR(@BeginDate), MONTH(@BeginDate), 1)     AS MonthDate
	UNION ALL
	SELECT DATEADD(MONTH, 1, MonthDate) FROM SubQuery
	WHERE DATEADD(MONTH, 1, MonthDate) <= @EndDate)
	SELECT MonthDate FROM SubQuery
GO
