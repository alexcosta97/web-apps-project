CREATE TABLE [dbo].[Direction]
(
	[DirectionID] Int Identity(1,1) Not Null,
	[DirectionName] NVarchar(50) Null,
	[Direction] Bit Not Null,
	[LineID] Int Not Null,
	Primary Key Clustered ([DirectionID] ASC),
	Constraint [FK_dbo.Direction_dbo.Line_LineID] Foreign Key ([LineID]) References [dbo].[Line] ([LineID]) On Delete Cascade
)
