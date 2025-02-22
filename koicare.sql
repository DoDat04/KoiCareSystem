CREATE DATABASE KoiCare;
GO

USE KoiCare;
GO


CREATE TABLE [dbo].[Role] (
    [RoleID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [RoleName] [varchar](255) NOT NULL,
    [Description] [nvarchar](255) NOT NULL,
    [isActive] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](6) NULL,
    [CreateBy] [varchar](255) NULL,
    [UpdateDate] [datetimeoffset](6) NULL,
    [UpdateBy] [varchar](255) NULL
	CONSTRAINT UQ_Role_RoleName UNIQUE (RoleName)
);

CREATE TABLE [dbo].[Member] (
    [MemberID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Password] [varchar](255) NOT NULL,
    [RoleID] [int] NOT NULL,
    [FirstName] [nvarchar](20) NOT NULL,
    [LastName] [nvarchar](20) NOT NULL,
    [Email] [nvarchar](255) NOT NULL,
    [PhoneNumber] [varchar](255) NOT NULL,
    [CreateBy] [varchar](255) NULL,
    [UpdateDate] [datetimeoffset](6) NULL,
    [UpdateBy] [varchar](255) NULL
	CONSTRAINT UQ_Member_Email UNIQUE (Email),
	[isActive] [bit] NOT NULL DEFAULT 1,
	[CreateDate] [datetime] NULL DEFAULT GETDATE(),
);

CREATE TABLE [dbo].[Ponds] (
    [PondID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] [nvarchar](50) NOT NULL,
    [MemberID] [int] NOT NULL,
    [Length] [decimal](10, 2) NOT NULL,
    [Width] [decimal](10, 2) NOT NULL,
    [Depth] [decimal](10, 2) NOT NULL,
	[ImagePath] [nvarchar](255) NULL,
    [isActive] [bit] NOT NULL,
    [CreateDate] [datetime] NULL,
    [CreateBy] [nvarchar](255) NULL,
    [UpdateDate] [datetime] NULL,
    [UpdateBy] [nvarchar](255) NULL
	CONSTRAINT FK_Ponds_Member FOREIGN KEY (MemberID) REFERENCES [dbo].[Member](MemberID)

);

CREATE TABLE [dbo].[Fish] (
    [FishID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [PondID] [int] NOT NULL,
    [MemberID] [int] NOT NULL,
    [Name] [nvarchar](50) NOT NULL,
    [Length] [decimal](10, 2) NOT NULL,
    [Weight] [decimal](10, 2) NOT NULL,
    [BirthDate] [datetime] NOT NULL, 
    [Gender] [nvarchar](10) NOT NULL,
    [Breed] [nvarchar](50) NOT NULL,
	[ImagePath] [nvarchar](255) NULL,  -- Add this line for the image path
    [isActive] [bit] NOT NULL DEFAULT 1,
    [CreateDate] [datetime] NULL DEFAULT GETDATE(),
    [CreateBy] [nvarchar](50) NULL,
    [UpdateDate] [datetime] NULL,
    [UpdateBy] [nvarchar](50) NULL,
    CONSTRAINT FK_Fish_Ponds FOREIGN KEY (PondID) REFERENCES [dbo].[Ponds](PondID),
    CONSTRAINT FK_Fish_Member FOREIGN KEY (MemberID) REFERENCES [dbo].[Member](MemberID)
);


CREATE TABLE [dbo].[FoodType] (
    [FoodTypeID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] [nvarchar](50) NOT NULL,
    [isActive] [bit] NOT NULL,
    [CreateDate] [datetime] NULL,
    [CreateBy] [nvarchar](50) NULL,
    [UpdateDate] [datetime] NULL,
    [UpdateBy] [nvarchar](50) NULL
);

CREATE TABLE [dbo].[GrowthRecord] (
    [RecordID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [FishID] [int] NOT NULL,
    [MeasurementDate] [date] NOT NULL,
    [Length] [decimal](10, 2) NOT NULL,
    [Weight] [decimal](10, 2) NOT NULL,
    [Description] [nvarchar](500) NULL,
    [isActive] [bit] NOT NULL,
    [CreateDate] [datetime] NULL,
    [CreateBy] [nvarchar](50) NULL,
    [UpdateDate] [datetime] NULL,
    [UpdateBy] [nvarchar](50) NULL
	CONSTRAINT FK_GrowthRecord_Fish FOREIGN KEY (FishID) REFERENCES [dbo].[Fish](FishID)
);

CREATE TABLE [dbo].[WaterParameters] (
    [ParameterID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [PondID] [int] NOT NULL,
    [MeasurementDate] [datetime] NOT NULL,
    [pH] [decimal](4, 2) NOT NULL,
    [Ammonia] [decimal](10, 2) NOT NULL, -- in mg/L
    [Nitrite] [decimal](10, 2) NOT NULL, -- in mg/L
    [Nitrate] [decimal](10, 2) NOT NULL, -- in mg/L
    [Temperature] [decimal](5, 2) NOT NULL, -- in Celsius
    [isActive] [bit] NOT NULL,
    [CreateDate] [datetime] NULL,
    [CreateBy] [nvarchar](50) NULL,
    [UpdateDate] [datetime] NULL,
    [UpdateBy] [nvarchar](50) NULL,
    CONSTRAINT FK_WaterParameters_Ponds FOREIGN KEY (PondID) REFERENCES [dbo].[Ponds](PondID)
);

CREATE TABLE [dbo].[FoodCalculator] (
    [CalcID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [FishID] [int] NULL,  -- Nullable for pond calculations
    [FoodTypeID] [int] NOT NULL, -- Reference to the food type
    [RecommendedAmount] [decimal](10, 2) NOT NULL, -- in grams
    [CalculationDate] [datetime] NOT NULL,
    [Notes] [nvarchar](500) NULL,
    CONSTRAINT FK_FoodCalculator_Fish FOREIGN KEY (FishID) REFERENCES [dbo].[Fish](FishID),
    CONSTRAINT FK_FoodCalculator_FoodType FOREIGN KEY (FoodTypeID) REFERENCES [dbo].[FoodType](FoodTypeID)
);

INSERT INTO [dbo].[Role] ([RoleName], [Description], [isActive], [CreateDate], [CreateBy])
VALUES 
('Admin', 'System Administrator with full access', 1, SYSDATETIMEOFFSET(), 'InitialSetup'),
('Member', 'Regular member with standard permissions', 1, SYSDATETIMEOFFSET(), 'InitialSetup'),
('Manager', 'Pond management role', 1, SYSDATETIMEOFFSET(), 'InitialSetup');

INSERT INTO [dbo].[Member] ([Password], [RoleID], [FirstName], [LastName], [Email], [PhoneNumber], [isActive])
VALUES 
('hashedpassword1', 1, 'John', 'Doe', 'john.doe@example.com', '555-1234', 1),
('hashedpassword2', 2, 'Jane', 'Smith', 'jane.smith@example.com', '555-5678', 1),
('hashedpassword3', 3, 'Mike', 'Johnson', 'mike.johnson@example.com', '555-9012', 1);

INSERT INTO [dbo].[Ponds] ([Name], [MemberID], [Length], [Width], [Depth], [isActive], [CreateDate])
VALUES 
('Koi Pond A', 1, 10.50, 5.25, 2.00, 1, GETDATE()),
('Trout Pond B', 2, 15.75, 8.00, 3.50, 1, GETDATE()),
('Goldfish Pond C', 3, 7.25, 4.00, 1.50, 1, GETDATE());

INSERT INTO [dbo].[FoodType] ([Name], [isActive], [CreateDate])
VALUES 
('Pellets', 1, GETDATE()),
('Flakes', 1, GETDATE()),
('Live Worms', 1, GETDATE()),
('Frozen Shrimp', 1, GETDATE());

INSERT INTO [dbo].[Fish] ([PondID], [MemberID], [Name], [Length], [Weight], [BirthDate], [Gender], [Breed], [isActive])
VALUES 
(1, 1, 'Goldie', 15.50, 2.25, '2022-03-15', 'Female', 'Koi', 1),
(2, 2, 'Rainbow', 20.75, 3.50, '2021-06-20', 'Male', 'Rainbow Trout', 1),
(3, 3, 'Bubbles', 10.25, 1.00, '2023-01-10', 'Female', 'Goldfish', 1);

INSERT INTO [dbo].[GrowthRecord] ([FishID], [MeasurementDate], [Length], [Weight], [Description], [isActive])
VALUES 
(1, '2023-06-15', 16.00, 2.50, 'Healthy growth observed', 1),
(2, '2023-06-15', 21.25, 3.75, 'Steady weight gain', 1),
(3, '2023-06-15', 10.50, 1.10, 'Normal development', 1);

INSERT INTO [dbo].[WaterParameters] ([PondID], [MeasurementDate], [pH], [Ammonia], [Nitrite], [Nitrate], [Temperature], [isActive])
VALUES 
(1, GETDATE(), 7.2, 0.25, 0.05, 5.00, 22.5, 1),
(2, GETDATE(), 6.8, 0.10, 0.02, 3.50, 15.0, 1),
(3, GETDATE(), 7.0, 0.15, 0.03, 4.00, 18.5, 1);

INSERT INTO [dbo].[FoodCalculator] ([FishID], [FoodTypeID], [RecommendedAmount], [CalculationDate], [Notes])
VALUES 
(1, 1, 25.50, GETDATE(), 'Daily feeding for Koi'),
(2, 2, 35.75, GETDATE(), 'Daily feeding for Trout'),
(3, 3, 15.25, GETDATE(), 'Daily feeding for Goldfish');