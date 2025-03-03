USE Trife

SELECT * FROM Machine
SELECT * FROM MachineIndicator
SELECT * FROM MachineIndicatorValues

/*
DELETE FROM MachineIndicatorValues
-- DBCC CHECKIDENT ('MachineIndicatorValues', RESEED, 0);
DELETE FROM MachineIndicator
-- DBCC CHECKIDENT ('MachineIndicator', RESEED, 0);
DELETE FROM Machine
-- DBCC CHECKIDENT ('Machine', RESEED, 0);
*/

/*
SELECT * FROM Machine M
INNER JOIN MachineIndicatorValues MIV ON M.Id = MIV.MachineId
INNER JOIN MachineIndicator MI ON MIV.MachineIndicatorId = MI.Id
*/

SELECT *
FROM (
    SELECT
        *,
        ROW_NUMBER() OVER (PARTITION BY CAST([Date] AS DATE) ORDER BY [Value] DESC) AS RowNum
    FROM
        MachineIndicatorValues
) AS RankedValues
WHERE RowNum = 1;
