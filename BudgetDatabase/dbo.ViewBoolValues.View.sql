USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ViewBoolValues]
AS
SELECT BoolValue from (VALUES (0), (1)) AS BoolVals(BoolValue)  
GO
