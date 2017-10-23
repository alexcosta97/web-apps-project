CREATE TABLE [dbo].[RouteStop]
(
	[RouteID] Int Not Null,
	[StopID] Int Not Null,
	[RouteStopTime] time Not Null,
	Constraint [FK_dbo.RouteStop_dbo.Route_RouteID] Foreign Key ([RouteID]) References [dbo].[Route] ([RouteID]) On Delete Cascade,
	Constraint [FK_dbo.RouteStop_dbo.Stops_StopID] Foreign Key ([StopID]) References [dbo].[Stops] ([StopID]) On Delete Cascade
)
