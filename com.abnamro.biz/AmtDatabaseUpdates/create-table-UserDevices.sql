USE [AMT_DEV]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF (EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'UserDevices'))
BEGIN
    DROP TABLE [dbo].[UserDevices]
END

CREATE TABLE [dbo].[UserDevices](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[DeviceId] [nvarchar](50) NOT NULL,	
	[IsoLanguageCode] [nchar](2) NOT NULL DEFAULT 'nl',
	[IsoCountryCode] [nchar](2) NOT NULL DEFAULT 'NL',
	[PincodeHash] [char](60) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,  	/* sort the data case sensitive; ..._CS_AS stands for: Case Sensitive and Accent Sensitive */
	[Description] [nvarchar](100) NOT NULL,	
	[LockedOut] bit NOT NULL DEFAULT 0,
 CONSTRAINT [PK_UserDevices] PRIMARY KEY NONCLUSTERED
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[UserDevices]  WITH CHECK ADD  CONSTRAINT [UQ_DeviceId] UNIQUE([DeviceId]) 
GO
ALTER TABLE [dbo].[UserDevices]  WITH CHECK ADD  CONSTRAINT [FK_UserDevices_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserDevices] CHECK CONSTRAINT [FK_UserDevices_Users]
GO


