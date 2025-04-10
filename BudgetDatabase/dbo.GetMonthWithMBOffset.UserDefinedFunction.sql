USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Dietrich Neuman
-- Create date: 13-Nov-24
-- Description:	Returns the "fiscal" month a given date is in, applying the configured MonthBoundaryOffset, as a date.
-- =============================================
CREATE FUNCTION [dbo].[GetMonthWithMBOffset] 
(
	-- Add the parameters for the function here
	@DateVal Date 
)
RETURNS Date
AS
BEGIN
	DECLARE @DateWithMBOffset Date;
	SET @DateWithMBOffset = DATEADD(day, (select MonthBoundaryOffset from dbo.BudgetConfig), @DateVal);

	RETURN DATEFROMPARTS(YEAR(@DateWithMBOffset), MONTH(@DateWithMBOffset), 1)

END
GO
