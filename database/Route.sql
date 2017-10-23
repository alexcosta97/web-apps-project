CREATE TABLE [dbo].[Route]
(
	[RouteID] Int Identity(1,1) Not Null,
	[RouteNote] NVarChar(100) Null,
	[DirectionID] Int Not Null,
	[EmployeeID] Int Null,
	[RouteStart] DateTime Null,
	[RouteEnd] DateTime Null,
	[RouteDuration] Float Null,
	Primary Key Clustered ([RouteID] ASC),
	Constraint [FK_dbo.Route_dbo.Staff_EmployeeID] Foreign Key ([EmployeeID]) References [dbo].[Staff] ([EmployeeID]) On Delete Set Null,
	Constraint [FK_dbo.Route_dbo.Direction_DirectionID] Foreign Key ([DirectionID]) References [dbo].[Direction] ([DirectionID]) On Delete Cascade
)
