use HotellGK

SELECT * FROM Rooms;


SELECT * FROM Rooms
WHERE IsAvailable = 1;


SELECT * FROM Rooms
ORDER BY RoomId ASC;


SELECT * FROM Bookings
ORDER BY CheckInDate ASC;


SELECT b.BookingId, b.CheckInDate, b.CheckOutDate, r.RoomType, g.Name AS GuestName
FROM Bookings b
JOIN Rooms r ON b.RoomId = r.RoomId
JOIN Guests g ON b.GuestId = g.GuestId;


SELECT * 
FROM Guests
ORDER BY [Name] DESC;


SELECT * 
FROM Rooms
WHERE RoomType = 'Double';








