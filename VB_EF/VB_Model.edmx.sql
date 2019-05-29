
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/27/2019 17:22:22
-- Generated from EDMX file: C:\Users\jcfds\Desktop\JR_Bookings\VB_EF\VB_Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [VB_Database];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ReservationRoom_Reservations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReservationRoom] DROP CONSTRAINT [FK_ReservationRoom_Reservations];
GO
IF OBJECT_ID(N'[dbo].[FK_ReservationRoom_Rooms]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReservationRoom] DROP CONSTRAINT [FK_ReservationRoom_Rooms];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomHotel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rooms] DROP CONSTRAINT [FK_RoomHotel];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomPicture]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pictures] DROP CONSTRAINT [FK_RoomPicture];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Hotels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Hotels];
GO
IF OBJECT_ID(N'[dbo].[Pictures]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pictures];
GO
IF OBJECT_ID(N'[dbo].[ReservationRoom]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReservationRoom];
GO
IF OBJECT_ID(N'[dbo].[Reservations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reservations];
GO
IF OBJECT_ID(N'[dbo].[Rooms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rooms];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Hotels'
CREATE TABLE [dbo].[Hotels] (
    [IdHotel] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Location] nvarchar(max)  NOT NULL,
    [Category] smallint  NOT NULL,
    [HasWifi] bit  NOT NULL,
    [HasParking] bit  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Website] nvarchar(max)  NOT NULL,
    [Capacity] int  NULL
);
GO

-- Creating table 'Pictures'
CREATE TABLE [dbo].[Pictures] (
    [IdPicture] int IDENTITY(1,1) NOT NULL,
    [Url] nvarchar(max)  NOT NULL,
    [IdRoom] int  NOT NULL
);
GO

-- Creating table 'Reservations'
CREATE TABLE [dbo].[Reservations] (
    [IdReservation] int IDENTITY(1,1) NOT NULL,
    [Firstname] nvarchar(max)  NOT NULL,
    [Lastname] nvarchar(max)  NOT NULL,
    [numberOfGuest] int  NOT NULL,
    [CheckIn] datetime  NOT NULL,
    [CheckOut] datetime  NOT NULL,
    [FinalPrice] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'Rooms'
CREATE TABLE [dbo].[Rooms] (
    [IdRoom] int IDENTITY(1,1) NOT NULL,
    [Number] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Type] smallint  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [HasTV] bit  NOT NULL,
    [HasHairDryer] bit  NOT NULL,
    [Hotel_IdHotel] int  NOT NULL
);
GO

-- Creating table 'ReservationRoom'
CREATE TABLE [dbo].[ReservationRoom] (
    [Reservations_IdReservation] int  NOT NULL,
    [Rooms_IdRoom] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdHotel] in table 'Hotels'
ALTER TABLE [dbo].[Hotels]
ADD CONSTRAINT [PK_Hotels]
    PRIMARY KEY CLUSTERED ([IdHotel] ASC);
GO

-- Creating primary key on [IdPicture] in table 'Pictures'
ALTER TABLE [dbo].[Pictures]
ADD CONSTRAINT [PK_Pictures]
    PRIMARY KEY CLUSTERED ([IdPicture] ASC);
GO

-- Creating primary key on [IdReservation] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [PK_Reservations]
    PRIMARY KEY CLUSTERED ([IdReservation] ASC);
GO

-- Creating primary key on [IdRoom] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [PK_Rooms]
    PRIMARY KEY CLUSTERED ([IdRoom] ASC);
GO

-- Creating primary key on [Reservations_IdReservation], [Rooms_IdRoom] in table 'ReservationRoom'
ALTER TABLE [dbo].[ReservationRoom]
ADD CONSTRAINT [PK_ReservationRoom]
    PRIMARY KEY CLUSTERED ([Reservations_IdReservation], [Rooms_IdRoom] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Hotel_IdHotel] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [FK_RoomHotel]
    FOREIGN KEY ([Hotel_IdHotel])
    REFERENCES [dbo].[Hotels]
        ([IdHotel])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomHotel'
CREATE INDEX [IX_FK_RoomHotel]
ON [dbo].[Rooms]
    ([Hotel_IdHotel]);
GO

-- Creating foreign key on [IdRoom] in table 'Pictures'
ALTER TABLE [dbo].[Pictures]
ADD CONSTRAINT [FK_RoomPicture]
    FOREIGN KEY ([IdRoom])
    REFERENCES [dbo].[Rooms]
        ([IdRoom])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomPicture'
CREATE INDEX [IX_FK_RoomPicture]
ON [dbo].[Pictures]
    ([IdRoom]);
GO

-- Creating foreign key on [Reservations_IdReservation] in table 'ReservationRoom'
ALTER TABLE [dbo].[ReservationRoom]
ADD CONSTRAINT [FK_ReservationRoom_Reservations]
    FOREIGN KEY ([Reservations_IdReservation])
    REFERENCES [dbo].[Reservations]
        ([IdReservation])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Rooms_IdRoom] in table 'ReservationRoom'
ALTER TABLE [dbo].[ReservationRoom]
ADD CONSTRAINT [FK_ReservationRoom_Rooms]
    FOREIGN KEY ([Rooms_IdRoom])
    REFERENCES [dbo].[Rooms]
        ([IdRoom])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReservationRoom_Rooms'
CREATE INDEX [IX_FK_ReservationRoom_Rooms]
ON [dbo].[ReservationRoom]
    ([Rooms_IdRoom]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------