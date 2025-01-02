using System;
using System.Collections.Generic;
using HotellGK.Services;
using HotellGK.Models;

namespace HotellGK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Säkerställ att databasen existerar
            using (var context = new HotelDbContext())
            {
                context.Database.EnsureCreated();
            }

            // Skapa tjänster manuellt
            var roomService = new RoomService(new HotelDbContext());
            var guestService = new GuestService(new HotelDbContext());
            var bookingService = new BookingService(new HotelDbContext());

            // Menylogik
            string[] menuOptions =
            {
                "Add Room",
                "Add Customer",
                "Add Booking",
                "Update Room",
                "Delete Room",
                "View Rooms",
                "View Guests",
                "View Bookings",
                "Exit"
            };

            int selectedOption = 0;

            while (true)
            {
                Console.Clear();
                Menu.DrawMenu(menuOptions, selectedOption);

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOption = (selectedOption == 0) ? menuOptions.Length - 1 : selectedOption - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        selectedOption = (selectedOption == menuOptions.Length - 1) ? 0 : selectedOption + 1;
                        break;

                    case ConsoleKey.Enter:
                        ExecuteMenuOption(menuOptions[selectedOption], roomService, guestService, bookingService);
                        break;
                }
            }
        }

        private static void ExecuteMenuOption(string selectedOption, RoomService roomService, GuestService guestService, BookingService bookingService)
        {
            Console.Clear();

            switch (selectedOption)
            {
                case "Add Room":
                    Console.Write("Enter Room Type (Single/Double): ");
                    var roomType = Console.ReadLine();
                    Console.Write("Has Extra Beds? (true/false): ");
                    var hasExtraBeds = bool.Parse(Console.ReadLine());
                    Console.Write("Enter Max Extra Beds (if any, 0 for none): ");
                    var maxExtraBeds = int.Parse(Console.ReadLine());

                    roomService.Add(new Room
                    {
                        RoomType = roomType,
                        HasExtraBeds = hasExtraBeds,
                        MaxExtraBeds = maxExtraBeds,
                        IsAvailable = true
                    });

                    Console.WriteLine("Room added successfully!");
                    Console.ReadKey();
                    break;

                case "Update Room":
                    Console.Write("Enter Room ID to update: ");
                    var roomIdToUpdate = int.Parse(Console.ReadLine());

                    Console.Write("Enter New Room Type (Single/Double): ");
                    var newRoomType = Console.ReadLine();
                    Console.Write("Has Extra Beds? (true/false): ");
                    var newHasExtraBeds = bool.Parse(Console.ReadLine());
                    Console.Write("Enter Max Extra Beds (if any, 0 for none): ");
                    var newMaxExtraBeds = int.Parse(Console.ReadLine());

                    roomService.Update(roomIdToUpdate, new Room
                    {
                        RoomType = newRoomType,
                        HasExtraBeds = newHasExtraBeds,
                        MaxExtraBeds = newMaxExtraBeds,
                        IsAvailable = true
                    });

                    Console.WriteLine("Room updated successfully!");
                    Console.ReadKey();
                    break;

                case "Delete Room":
                    Console.Write("Enter Room ID to delete: ");
                    var roomIdToDelete = int.Parse(Console.ReadLine());

                    // Kontrollera om rummet kan tas bort
                    roomService.Delete(roomIdToDelete);
                    Console.ReadKey();
                    break;

                case "Add Customer":
                    Console.Write("Enter Customer Name: ");
                    var customerName = Console.ReadLine();
                    Console.Write("Enter Customer Email: ");
                    var customerEmail = Console.ReadLine();
                    Console.Write("Enter Customer Phone Number: ");
                    var customerPhone = Console.ReadLine();
                    guestService.Add(new Guest { Name = customerName, Email = customerEmail, PhoneNumber = customerPhone });
                    Console.WriteLine("Customer added successfully!");
                    Console.ReadKey();
                    break;

                case "Add Booking":
                    Console.Write("Enter Room ID: ");
                    var roomId = int.Parse(Console.ReadLine());
                    Console.Write("Enter Guest ID: ");
                    var guestId = int.Parse(Console.ReadLine());
                    Console.Write("Enter Check-In Date (yyyy-mm-dd): ");
                    var checkIn = DateTime.Parse(Console.ReadLine());
                    Console.Write("Enter Check-Out Date (yyyy-mm-dd): ");
                    var checkOut = DateTime.Parse(Console.ReadLine());
                    bookingService.Add(new Booking { RoomId = roomId, GuestId = guestId, CheckInDate = checkIn, CheckOutDate = checkOut });
                    Console.WriteLine("Booking added successfully!");
                    Console.ReadKey();
                    break;

                case "View Rooms":
                    var rooms = roomService.GetAll();
                    foreach (var room in rooms)
                    {
                        Console.WriteLine($"ID: {room.RoomId}, Type: {room.RoomType}, Extra Beds: {room.HasExtraBeds}, Available: {room.IsAvailable}");
                    }
                    Console.ReadKey();
                    break;

                case "View Customers":
                    var customers = guestService.GetAll();
                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"ID: {customer.GuestId}, Name: {customer.Name}, Email: {customer.Email}, Phone: {customer.PhoneNumber}");
                    }
                    Console.ReadKey();
                    break;

                case "View Bookings":
                    var bookings = bookingService.GetAll();
                    foreach (var booking in bookings)
                    {
                        Console.WriteLine($"ID: {booking.BookingId}, Room ID: {booking.RoomId}, Guest ID: {booking.GuestId}, Check-In: {booking.CheckInDate}, Check-Out: {booking.CheckOutDate}");
                    }
                    Console.ReadKey();
                    break;

                case "Exit":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid option!");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
