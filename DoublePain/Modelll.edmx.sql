
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/13/2018 18:23:41
-- Generated from EDMX file: C:\Users\Xiaomi\Desktop\Учеба\2 курс\DoublePain\DoublePain\Modelll.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DBPain];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TypeOfRoomRoom]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoomSet] DROP CONSTRAINT [FK_TypeOfRoomRoom];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VisitSet] DROP CONSTRAINT [FK_RoomVisit];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VisitSet] DROP CONSTRAINT [FK_ClientVisit];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientService]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServiceSet] DROP CONSTRAINT [FK_ClientService];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeService]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ServiceSet] DROP CONSTRAINT [FK_EmployeeService];
GO
IF OBJECT_ID(N'[dbo].[FK_TypeOfServiceEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeSet] DROP CONSTRAINT [FK_TypeOfServiceEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_JobEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeSet] DROP CONSTRAINT [FK_JobEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_TypeOfPriceTypeOfService]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TypeOfServiceSet] DROP CONSTRAINT [FK_TypeOfPriceTypeOfService];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[VisitSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VisitSet];
GO
IF OBJECT_ID(N'[dbo].[ServiceSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ServiceSet];
GO
IF OBJECT_ID(N'[dbo].[TypeOfRoomSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeOfRoomSet];
GO
IF OBJECT_ID(N'[dbo].[RoomSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoomSet];
GO
IF OBJECT_ID(N'[dbo].[JobSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobSet];
GO
IF OBJECT_ID(N'[dbo].[TypeOfServiceSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeOfServiceSet];
GO
IF OBJECT_ID(N'[dbo].[TypeOfPriceSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeOfPriceSet];
GO
IF OBJECT_ID(N'[dbo].[ClientSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientSet];
GO
IF OBJECT_ID(N'[dbo].[EmployeeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'VisitSet'
CREATE TABLE [dbo].[VisitSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstDate] datetime  NOT NULL,
    [SecondDate] datetime  NOT NULL,
    [Room_Id] int  NOT NULL,
    [Client_Id] int  NOT NULL
);
GO

-- Creating table 'ServiceSet'
CREATE TABLE [dbo].[ServiceSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [NumberOfPeople] int  NOT NULL,
    [NumberOfHours] int  NOT NULL,
    [Client_Id] int  NOT NULL,
    [Employee_Id] int  NOT NULL
);
GO

-- Creating table 'TypeOfRoomSet'
CREATE TABLE [dbo].[TypeOfRoomSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Capacity] int  NOT NULL,
    [Price] int  NOT NULL
);
GO

-- Creating table 'RoomSet'
CREATE TABLE [dbo].[RoomSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [TypeOfRoom_Id] int  NOT NULL
);
GO

-- Creating table 'JobSet'
CREATE TABLE [dbo].[JobSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [JobName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TypeOfServiceSet'
CREATE TABLE [dbo].[TypeOfServiceSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] int  NOT NULL,
    [TypeOfPrice_Id] int  NOT NULL
);
GO

-- Creating table 'TypeOfPriceSet'
CREATE TABLE [dbo].[TypeOfPriceSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ClientSet'
CREATE TABLE [dbo].[ClientSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [DateBirth] datetime  NOT NULL,
    [Sex] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Passport] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EmployeeSet'
CREATE TABLE [dbo].[EmployeeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [DateBirth] datetime  NOT NULL,
    [Sex] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Passport] nvarchar(max)  NOT NULL,
    [TypeOfService_Id] int  NOT NULL,
    [Job_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'VisitSet'
ALTER TABLE [dbo].[VisitSet]
ADD CONSTRAINT [PK_VisitSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ServiceSet'
ALTER TABLE [dbo].[ServiceSet]
ADD CONSTRAINT [PK_ServiceSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TypeOfRoomSet'
ALTER TABLE [dbo].[TypeOfRoomSet]
ADD CONSTRAINT [PK_TypeOfRoomSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RoomSet'
ALTER TABLE [dbo].[RoomSet]
ADD CONSTRAINT [PK_RoomSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobSet'
ALTER TABLE [dbo].[JobSet]
ADD CONSTRAINT [PK_JobSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TypeOfServiceSet'
ALTER TABLE [dbo].[TypeOfServiceSet]
ADD CONSTRAINT [PK_TypeOfServiceSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TypeOfPriceSet'
ALTER TABLE [dbo].[TypeOfPriceSet]
ADD CONSTRAINT [PK_TypeOfPriceSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClientSet'
ALTER TABLE [dbo].[ClientSet]
ADD CONSTRAINT [PK_ClientSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [PK_EmployeeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TypeOfRoom_Id] in table 'RoomSet'
ALTER TABLE [dbo].[RoomSet]
ADD CONSTRAINT [FK_TypeOfRoomRoom]
    FOREIGN KEY ([TypeOfRoom_Id])
    REFERENCES [dbo].[TypeOfRoomSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TypeOfRoomRoom'
CREATE INDEX [IX_FK_TypeOfRoomRoom]
ON [dbo].[RoomSet]
    ([TypeOfRoom_Id]);
GO

-- Creating foreign key on [Room_Id] in table 'VisitSet'
ALTER TABLE [dbo].[VisitSet]
ADD CONSTRAINT [FK_RoomVisit]
    FOREIGN KEY ([Room_Id])
    REFERENCES [dbo].[RoomSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomVisit'
CREATE INDEX [IX_FK_RoomVisit]
ON [dbo].[VisitSet]
    ([Room_Id]);
GO

-- Creating foreign key on [Client_Id] in table 'VisitSet'
ALTER TABLE [dbo].[VisitSet]
ADD CONSTRAINT [FK_ClientVisit]
    FOREIGN KEY ([Client_Id])
    REFERENCES [dbo].[ClientSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientVisit'
CREATE INDEX [IX_FK_ClientVisit]
ON [dbo].[VisitSet]
    ([Client_Id]);
GO

-- Creating foreign key on [Client_Id] in table 'ServiceSet'
ALTER TABLE [dbo].[ServiceSet]
ADD CONSTRAINT [FK_ClientService]
    FOREIGN KEY ([Client_Id])
    REFERENCES [dbo].[ClientSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientService'
CREATE INDEX [IX_FK_ClientService]
ON [dbo].[ServiceSet]
    ([Client_Id]);
GO

-- Creating foreign key on [Employee_Id] in table 'ServiceSet'
ALTER TABLE [dbo].[ServiceSet]
ADD CONSTRAINT [FK_EmployeeService]
    FOREIGN KEY ([Employee_Id])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeService'
CREATE INDEX [IX_FK_EmployeeService]
ON [dbo].[ServiceSet]
    ([Employee_Id]);
GO

-- Creating foreign key on [TypeOfService_Id] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [FK_TypeOfServiceEmployee]
    FOREIGN KEY ([TypeOfService_Id])
    REFERENCES [dbo].[TypeOfServiceSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TypeOfServiceEmployee'
CREATE INDEX [IX_FK_TypeOfServiceEmployee]
ON [dbo].[EmployeeSet]
    ([TypeOfService_Id]);
GO

-- Creating foreign key on [Job_Id] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [FK_JobEmployee]
    FOREIGN KEY ([Job_Id])
    REFERENCES [dbo].[JobSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobEmployee'
CREATE INDEX [IX_FK_JobEmployee]
ON [dbo].[EmployeeSet]
    ([Job_Id]);
GO

-- Creating foreign key on [TypeOfPrice_Id] in table 'TypeOfServiceSet'
ALTER TABLE [dbo].[TypeOfServiceSet]
ADD CONSTRAINT [FK_TypeOfPriceTypeOfService]
    FOREIGN KEY ([TypeOfPrice_Id])
    REFERENCES [dbo].[TypeOfPriceSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TypeOfPriceTypeOfService'
CREATE INDEX [IX_FK_TypeOfPriceTypeOfService]
ON [dbo].[TypeOfServiceSet]
    ([TypeOfPrice_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------