CREATE TABLE [dbo].[contact](
	[contact_id] [int] IDENTITY(1,1) NOT NULL,
	[contact_first_name] [varchar](50) NOT NULL,
	[contact_last_name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_dbo.contact] PRIMARY KEY ([contact_id] ASC)
)
GO

CREATE TABLE [dbo].[contact_email](
	[email] [varchar](100) NOT NULL,
	[contact_id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.contact_email] PRIMARY KEY ([email] ASC)
)
GO

ALTER TABLE [dbo].[contact_email]  WITH CHECK ADD  CONSTRAINT [FK_dbo.contact_email_dbo.contact_contact_id] FOREIGN KEY([contact_id])
REFERENCES [dbo].[contact] ([contact_id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[contact_email] CHECK CONSTRAINT [FK_dbo.contact_email_dbo.contact_contact_id]
GO