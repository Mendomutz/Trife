USE Trife

/*
DROP TABLE MachineIndicatorValues
DROP TABLE MachineIndicator
DROP TABLE Machine
*/

CREATE TABLE Machine (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(255),
	MachineIndicators JSON,
	IsEnabled BIT,
	Deleted BIT
);

CREATE TABLE MachineIndicator (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(255),
	Unit NVARCHAR(32),
	Type INT,
	IsEnabled BIT,
	Deleted BIT
);

CREATE TABLE MachineIndicatorValues (
	Id INT PRIMARY KEY IDENTITY,
	MachineId INT,
	MachineIndicatorId INT,
	Value FLOAT,
	Date  DATETIME,
	IsEnabled BIT,
	Deleted BIT
);
