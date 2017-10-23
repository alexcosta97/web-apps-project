CREATE TABLE [dbo].[FavRoutes]
(
	[userID] Int Not Null,
	[RouteID] Int Not Null,
	[FavName] NVarChar(100) Null,
	Constraint [FK_dbo.FavRoutes_dbo.Users_UserID] Foreign Key ([UserID]) References [dbo].[Users] ([UserID]) On Delete Cascade,
	Constraint [FK_dbo.FavRoutes_dbo.Route_RouteID] Foreign Key ([RouteID]) References [dbo].[Route] ([RouteID]) On Delete Cascade
)
