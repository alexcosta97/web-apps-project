CREATE TABLE [dbo].[Line]
(
	[LineID]	INT	Identity(1,1)	Not Null,
	[LineName]	NVarchar(50) Null,
	[LineStart] Date Null,
	[LineEnd] Date Null,
	Primary Key Clustered ([LineID] ASC)
)
