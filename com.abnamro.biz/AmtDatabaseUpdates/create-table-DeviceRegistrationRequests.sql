USE [AMT_DEV]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF (EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'DeviceRegistrationRequests'))
BEGIN
    DROP TABLE [dbo].[DeviceRegistrationRequests]
END
CREATE TABLE [dbo].[DeviceRegistrationRequests]
(
	[Id] [int] IDENTITY(1,1) Not Null,
	[UserId] [int] Not Null,
	[DeviceId] [nvarchar](50) Not Null,
	[RecipientEmailaddress] [varchar](150) Not Null,
	[RegistrationCode] [varchar](10) Not Null,
	[DeviceDescription] [nvarchar](100) NOT NULL,	
	[EmailSentDateTime] [DateTime] Not Null Default GetDate(),
	CONSTRAINT [PK_DeviceRegistrationCodes] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	) WITH (PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS=ON, ALLOW_PAGE_LOCKS=ON) ON [PRIMARY]
) On [PRIMARY]
GO

ALTER TABLE [dbo].[DeviceRegistrationRequests] WITH CHECK ADD  CONSTRAINT [FK_DeviceRegistrationRequests_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[DeviceRegistrationRequests]  WITH CHECK ADD  CONSTRAINT [UQ_DeviceRegistrationRequests_DeviceId] UNIQUE([DeviceId]) 
GO
