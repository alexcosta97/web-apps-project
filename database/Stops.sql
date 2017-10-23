CREATE TABLE [dbo].[Stops]
(
	[StopID] Int Identity(1,1) Not Null,
	[StopName] NVarChar(50) Null,
	Primary Key Clustered ([StopID] ASC)
)
