﻿CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[UserName] NVARCHAR(400) NOT NULL, 
	[Password] NVARCHAR(MAX) NOT NULL, 
    [Salt] VARBINARY(32) NOT NULL, 
    CONSTRAINT [AK_User_UserName] UNIQUE ([UserName]) 
)
