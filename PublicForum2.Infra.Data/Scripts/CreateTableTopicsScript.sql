
CREATE TABLE [dbo].[Topics](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[OwnerId] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Topics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Topics] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO