USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO













CREATE VIEW [dbo].[ViewGroupings]
AS

SELECT GroupingType, Subkey, RTRIM(CONCAT(GroupingType, Subkey)) AS GroupingKey, GroupingLabel, 
CASE WHEN ParentSubkey IS NULL THEN NULL ELSE RTRIM(CONCAT(GroupingType, ParentSubkey)) END AS ParentKey, -- ParentGroupingType is same as child's GroupingType as of 20-Jan-25 
IsTopLevel, GraphColor, SelectorTypeOrder
FROM

-- Groupings from TransacType table (assigned to Transactions):
(SELECT 
	  'T' AS GroupingType
      ,G.TRTypeID AS Subkey
      ,G.[GroupingLabel] AS GroupingLabel
	  ,G.ParentGroupingID AS ParentSubkey
	  ,G.IsParentGrouping AS IsTopLevel
	  ,G.GraphColor
	  ,CASE WHEN G.TRTypeID = 'IN_' THEN 1 ELSE 100 END AS SelectorTypeOrder
  FROM [TransacType] AS G
  LEFT OUTER JOIN [TransacType] AS PAR ON G.ParentGroupingID = PAR.TRTypeID
UNION 
-- Groupings for balances of each Account:
SELECT 'B' AS GroupingType, AccountID AS Subkey, CONCAT('Balance, ', AccountName) AS GroupingLabel, '_' AS ParentSubkey,
  0 AS IsTopLevel, GraphColor, 100 AS SelectorTypeOrder
  FROM Account
UNION 
-- Special grouping for total balances of all Accounts:
SELECT 'B' AS GroupingType, '_' AS Subkey, 'Balance' AS GroupingLabel, NULL AS ParentSubkey, 1 AS IsTopLevel, 'Blue' AS GraphColor, 3 AS SelectorTypeOrder
UNION 
-- Special grouping for total expenses:
SELECT 'E' AS GroupingType, '_' AS Subkey, 'Expenses' AS GroupingLabel, NULL AS ParentSubkey, 1 AS IsTopLevel, 'Red' AS GraphColor, 2 AS SelectorTypeOrder
) AS InnerQuery
GO
