using HotellGK.MenuClasses;
using HotellGK.Models;
using HotellGK.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellGK.Controllers
{
    public class BookingController
    {
        private readonly BookingService _bookingService;
        private readonly Menu _menu;

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
            _menu = new Menu();
        }

        public void AddBooking()
        {
            var rooms = _bookingService.GetAllAvailableRooms();

            if (!rooms.Any())
            {
                Console.WriteLine("No available rooms for booking.");
                Console.ReadKey();
                return;
            }

            var roomOptions = rooms.Select(r => $"{r.RoomId}: {r.RoomType}, {r.RoomSize}, Extra Beds: {(r.HasExtraBeds ? "Yes" : "No")}").ToArray();
            string selectedRoom = _menu.DrawMenuController(roomOptions, "Select a room to book:");
            int roomId = int.Parse(selectedRoom.Split(':')[0]);

            Console.Write("Enter Guest ID: ");
            int guestId = int.Parse(Console.ReadLine());

            Console.Write("Enter Check-In Date (yyyy-mm-dd): ");
            DateTime checkIn = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Check-Out Date (yyyy-mm-dd): ");
            DateTime checkOut = DateTime.Parse(Console.ReadLine());

            if (!_bookingService.IsRoomAvailable(roomId, checkIn, checkOut))
            {
                Console.WriteLine("Room is not available for the selected dates.");
                Console.ReadKey();
                return;
            }

            _bookingService.Add(new Booking
            {
                RoomId = roomId,
                GuestId = guestId,
                CheckInDate = checkIn,
                CheckOutDate = checkOut
            });

            Console.WriteLine("Booking added successfully! Press any key to continue...");
            
        }


        public void ViewBookings()
            {
                var bookings = _bookingService.GetAll();
                foreach (var booking in bookings)
                {
                    Console.WriteLine($"" +
                        $"ID: {booking.BookingId}, " +
                        $"Room ID: {booking.RoomId}, " +
                        $"Guest ID: {booking.GuestId}, " +
                        $"Check-In: {booking.CheckInDate}, " +
                        $"Check-Out: {booking.CheckOutDate}");
                }
            }
        }
    }
