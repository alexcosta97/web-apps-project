CREATE TABLE [dbo].[Staff]
(
	[EmployeeID] Int Identity(1,1) Not Null,
	[UserID] Int Not Null,
	[NINumber] NVarChar(9) Not Null,
	[AccountNumber] NVarChar(8) Not Null,
	[SortCode] NVarChar(6) Not Null,
	[Role] NVarChar(30) Null,
	[HoursContracted] Int Null,
	Primary Key Clustered([EmployeeID] ASC),
	Constraint [FK_dbo.Staff_dbo.Users_UserID] Foreign Key ([UserID]) References [dbo].[Users] ([UserID]) On Delete Cascade
)
