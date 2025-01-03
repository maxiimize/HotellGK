-- Exported from QuickDBD: https://www.quickdatabasediagrams.com/
-- Link to schema: https://app.quickdatabasediagrams.com/#/d/9qqr0t
-- NOTE! If you have used non-SQL datatypes in your design, you will have to change these here.


SET XACT_ABORT ON

BEGIN TRANSACTION QUICKDBD

CREATE TABLE [Rooms] (
    [RoomId] INT  NOT NULL ,
    [RoomType] NVARCHAR(50)  NOT NULL ,
    [RoomSize] NVARCHAR(50)  NOT NULL ,
    [HasExtraBeds] BIT  NOT NULL ,
    [MaxExtraBeds] INT  NOT NULL ,
    [IsAvailable] BIT  NOT NULL ,
    CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED (
        [RoomId] ASC
    )
)

CREATE TABLE [Guests] (
    [GuestId] INT  NOT NULL ,
    [Name] NVARCHAR(50)  NOT NULL ,
    [Email] NVARCHAR(50)  NOT NULL ,
    [PhoneNumber] NVARCHAR(20)  NOT NULL ,
    CONSTRAINT [PK_Guests] PRIMARY KEY CLUSTERED (
        [GuestId] ASC
    )
)

CREATE TABLE [Bookings] (
    [BookingId] INT  NOT NULL ,
    [RoomId] INT  NOT NULL ,
    [GuestId] INT  NOT NULL ,
    [CheckInDate] DATE  NOT NULL ,
    [CheckOutDate] DATE  NOT NULL ,
    CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED (
        [BookingId] ASC
    )
)

ALTER TABLE [Rooms] WITH CHECK ADD CONSTRAINT [FK_Rooms_RoomId] FOREIGN KEY([RoomId])
REFERENCES [Bookings] ([BookingId])

ALTER TABLE [Rooms] CHECK CONSTRAINT [FK_Rooms_RoomId]

ALTER TABLE [Guests] WITH CHECK ADD CONSTRAINT [FK_Guests_GuestId] FOREIGN KEY([GuestId])
REFERENCES [Bookings] ([BookingId])

ALTER TABLE [Guests] CHECK CONSTRAINT [FK_Guests_GuestId]

ALTER TABLE [Bookings] WITH CHECK ADD CONSTRAINT [FK_Bookings_RoomId] FOREIGN KEY([RoomId])
REFERENCES [Rooms] ([RoomId])

ALTER TABLE [Bookings] CHECK CONSTRAINT [FK_Bookings_RoomId]

ALTER TABLE [Bookings] WITH CHECK ADD CONSTRAINT [FK_Bookings_GuestId] FOREIGN KEY([GuestId])
REFERENCES [Guests] ([GuestId])

ALTER TABLE [Bookings] CHECK CONSTRAINT [FK_Bookings_GuestId]

COMMIT TRANSACTION QUICKDBD