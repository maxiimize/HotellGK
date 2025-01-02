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
                "Update Room",
                "View Rooms",
                "Add Customer",
                "Update Customer",
                "Add Booking",
                "View Guests",
                "View Bookings",
                "Delete Room",
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
                    Console.Write("Enter Room Size (Normal/Large): ");
                    var roomSize = Console.ReadLine();
                    Console.Write("Has Extra Beds? (true/false): ");
                    var hasExtraBeds = bool.Parse(Console.ReadLine());

                    int maxExtraBeds = 0;
                    if (hasExtraBeds)
                    {
                        if (roomType.Equals("Double", StringComparison.OrdinalIgnoreCase))
                        {
                            if (roomSize.Equals("Normal", StringComparison.OrdinalIgnoreCase))
                            {
                                Console.Write("Enter Max Extra Beds (0-1): ");
                                maxExtraBeds = int.Parse(Console.ReadLine());
                                if (maxExtraBeds < 0 || maxExtraBeds > 1)
                                {
                                    Console.WriteLine("Invalid number of extra beds for a Normal Double room. Must be 0 or 1.");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                            else if (roomSize.Equals("Large", StringComparison.OrdinalIgnoreCase))
                            {
                                Console.Write("Enter Max Extra Beds (0-2): ");
                                maxExtraBeds = int.Parse(Console.ReadLine());
                                if (maxExtraBeds < 0 || maxExtraBeds > 2)
                                {
                                    Console.WriteLine("Invalid number of extra beds for a Large Double room. Must be 0, 1, or 2.");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid room size. Must be 'Normal' or 'Large'.");
                                Console.ReadKey();
                                break;
                            }
                        }
                        else if (roomType.Equals("Single", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Extra beds are not allowed in Single rooms.");
                            hasExtraBeds = false;
                            maxExtraBeds = 0;
                        }
                        else
                        {
                            Console.WriteLine("Invalid room type. Must be 'Single' or 'Double'.");
                            Console.ReadKey();
                            break;
                        }
                    }

                    roomService.Add(new Room
                    {
                        RoomType = roomType,
                        RoomSize = roomSize,
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
                    Console.Write("Enter New Room Size (Normal/Large): ");
                    var newRoomSize = Console.ReadLine();
                    Console.Write("Has Extra Beds? (true/false): ");
                    var newHasExtraBeds = bool.Parse(Console.ReadLine());

                    int newMaxExtraBeds = 0;
                    if (newHasExtraBeds)
                    {
                        if (newRoomType.Equals("Double", StringComparison.OrdinalIgnoreCase))
                        {
                            if (newRoomSize.Equals("Normal", StringComparison.OrdinalIgnoreCase))
                            {
                                Console.Write("Enter Max Extra Beds (0-1): ");
                                newMaxExtraBeds = int.Parse(Console.ReadLine());
                                if (newMaxExtraBeds < 0 || newMaxExtraBeds > 1)
                                {
                                    Console.WriteLine("Invalid number of extra beds for a Normal Double room. Must be 0 or 1.");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                            else if (newRoomSize.Equals("Large", StringComparison.OrdinalIgnoreCase))
                            {
                                Console.Write("Enter Max Extra Beds (0-2): ");
                                newMaxExtraBeds = int.Parse(Console.ReadLine());
                                if (newMaxExtraBeds < 0 || newMaxExtraBeds > 2)
                                {
                                    Console.WriteLine("Invalid number of extra beds for a Large Double room. Must be 0, 1, or 2.");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid room size. Must be 'Normal' or 'Large'.");
                                Console.ReadKey();
                                break;
                            }
                        }
                        else if (newRoomType.Equals("Single", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Extra beds are not allowed in Single rooms.");
                            newHasExtraBeds = false;
                            newMaxExtraBeds = 0;
                        }
                        else
                        {
                            Console.WriteLine("Invalid room type. Must be 'Single' or 'Double'.");
                            Console.ReadKey();
                            break;
                        }
                    }

                    roomService.Update(roomIdToUpdate, new Room
                    {
                        RoomType = newRoomType,
                        RoomSize = newRoomSize,
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

                case "Update Customer":
                    Console.Write("Enter Customer ID to update: ");
                    var customerIdToUpdate = int.Parse(Console.ReadLine());

                    var customer = guestService.GetAll().FirstOrDefault(g => g.GuestId == customerIdToUpdate);
                    if (customer == null)
                    {
                        Console.WriteLine("Customer not found.");
                        Console.ReadKey();
                        break;
                    }

                    Console.Write($"Enter New Name (current: {customer.Name}): ");
                    var newName = Console.ReadLine();
                    Console.Write($"Enter New Email (current: {customer.Email}): ");
                    var newEmail = Console.ReadLine();
                    Console.Write($"Enter New Phone Number (current: {customer.PhoneNumber}): ");
                    var newPhone = Console.ReadLine();

                    guestService.Update(customerIdToUpdate, new Guest
                    {
                        Name = string.IsNullOrWhiteSpace(newName) ? customer.Name : newName,
                        Email = string.IsNullOrWhiteSpace(newEmail) ? customer.Email : newEmail,
                        PhoneNumber = string.IsNullOrWhiteSpace(newPhone) ? customer.PhoneNumber : newPhone
                    });

                    Console.WriteLine("Customer updated successfully!");
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
                        Console.WriteLine($"" +
                            $"ID: {room.RoomId}, " +
                            $"Type: {room.RoomType}, " +
                            $"Size: {room.RoomSize}, " +
                            $"Extra Beds: {room.HasExtraBeds}, " +
                            $"Max Extra Beds: {room.MaxExtraBeds}, " +
                            $"Available: {room.IsAvailable}");
                    }
                    Console.ReadKey();
                    break;



                case "View Customers":
                    var customers = guestService.GetAll();
                    foreach (var viewCustomer in customers)
                    {
                        Console.WriteLine($"" +
                            $"ID: {viewCustomer.GuestId}, " +
                            $"Name: {viewCustomer.Name}, " +
                            $"Email: {viewCustomer.Email}, " +
                            $"Phone: {viewCustomer.PhoneNumber}");
                    }
                    Console.ReadKey();
                    break;

                case "View Bookings":
                    var bookings = bookingService.GetAll();
                    foreach (var booking in bookings)
                    {
                        Console.WriteLine($"" +
                            $"ID: {booking.BookingId}, " +
                            $"Room ID: {booking.RoomId}, " +
                            $"Guest ID: {booking.GuestId}, " +
                            $"Check-In: {booking.CheckInDate}, " +
                            $"Check-Out: {booking.CheckOutDate}");
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
