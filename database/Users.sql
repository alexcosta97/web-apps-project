CREATE TABLE [dbo].[Users]
(
	[UserID] Int Identity(1,1) Not Null,
	[FirstName] NVarChar(50) Null,
	[SurName] NVarChar(50) Null,
	[PhoneNumber] NVarChar(15) Null,
	[eMail] NVarChar(200) Null,
	Primary Key Clustered([UserID] ASC)
)
