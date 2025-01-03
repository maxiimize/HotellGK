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

            public BookingController(BookingService bookingService)
            {
                _bookingService = bookingService;
            }

            public void AddBooking()
            {
                Console.Write("Enter Room ID: ");
                var roomId = int.Parse(Console.ReadLine());
                Console.Write("Enter Guest ID: ");
                var guestId = int.Parse(Console.ReadLine());
                Console.Write("Enter Check-In Date (yyyy-mm-dd): ");
                var checkIn = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter Check-Out Date (yyyy-mm-dd): ");
                var checkOut = DateTime.Parse(Console.ReadLine());

                _bookingService.Add(new Booking
                {
                    RoomId = roomId,
                    GuestId = guestId,
                    CheckInDate = checkIn,
                    CheckOutDate = checkOut
                });

                Console.WriteLine("Booking added successfully!");
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
