CREATE TABLE [dbo].[FavLines]
(
	[userID] Int Not Null,
	[LineID] Int Not Null,
	[FavName] NVarChar(100) Null,
	Constraint [FK_dbo.FavLines_dbo.Users_UserID] Foreign Key ([UserID]) References [dbo].[Users] ([UserID]) On Delete Cascade,
	Constraint [FK_dbo.FavLines_dbo.Line_LineID] Foreign Key ([LineID]) References [dbo].[Line] ([LineID]) On Delete Cascade
)
