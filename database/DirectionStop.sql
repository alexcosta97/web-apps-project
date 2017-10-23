CREATE TABLE [dbo].[DirectionStop]
(
	[DirectionID] Int Not Null,
	[StopID] Int Not Null,
	Constraint [FK_dbo.DirectionStop_dbo.Stops_UserID] Foreign Key ([StopID]) References [dbo].[Stops] ([StopID]) On Delete Cascade,
	Constraint [FK_dbo.DirectionStop_dbo.Direction_DirectionID] Foreign Key ([DirectionID]) References [dbo].[Direction] ([DirectionID]) On Delete Cascade
)
