CREATE TABLE [dbo].[Addresses]
(
	[UserID] Int Not Null,
	[Street1] NVarChar(100) Null,
	[Street2] NVarChar(100) Null,
	[PostalCode] NVarChar(10) Null,
	[County] NVarChar(30) Null,
	Constraint [FK_dbo.Addresses_dbo.Users_UserID] Foreign Key ([UserID]) References [dbo].[Users] ([UserID]) On Delete Cascade
)
