/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
GO
:setvar DatabaseName "FreeParkingSystem.Database"

GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)]
GO
SET IDENTITY_INSERT [dbo].[ParkingSpotType] ON 
GO
INSERT [dbo].[ParkingSpotType] ([Id], [Name]) VALUES (1, N'Bike')
GO
INSERT [dbo].[ParkingSpotType] ([Id], [Name]) VALUES (2, N'Car')
GO
INSERT [dbo].[ParkingSpotType] ([Id], [Name]) VALUES (3, N'Car for Disabled')
GO
INSERT [dbo].[ParkingSpotType] ([Id], [Name]) VALUES (4, N'VIP')
GO
SET IDENTITY_INSERT [dbo].[ParkingSpotType] OFF
GO
SET IDENTITY_INSERT [dbo].[ParkingType] ON 
GO
INSERT [dbo].[ParkingType] ([Id], [Name]) VALUES (1, N'Free')
GO
INSERT [dbo].[ParkingType] ([Id], [Name]) VALUES (2, N'Paid')
GO
INSERT [dbo].[ParkingType] ([Id], [Name]) VALUES (3, N'Public')
GO
SET IDENTITY_INSERT [dbo].[ParkingType] OFF
GO
