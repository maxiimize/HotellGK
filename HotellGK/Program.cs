using System;
using System.Collections.Generic;
using HotellGK.Services;
using HotellGK.Models;
using HotellGK.Controllers;

namespace HotellGK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            using (var context = new HotelDbContext())
            {
                context.Database.EnsureCreated();
            }

            
            var roomService = new RoomService(new HotelDbContext());
            var guestService = new GuestService(new HotelDbContext());
            var bookingService = new BookingService(new HotelDbContext());

            
            string[] menuOptions =
            {
                "Add Room",
                "Update Room",
                "View Rooms",
                "Add Customer",
                "Update Customer",
                "Add Booking",
                "View Customers",
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
            var roomController = new RoomController(roomService);
            var guestController = new GuestController(guestService);
            var bookingController = new BookingController(bookingService);


            Console.Clear();

            switch (selectedOption)
            {
                case "Add Room":
                    roomController.AddRoom();
                    Console.ReadKey();
                    break;

                case "Update Room":
                    roomController.UpdateRoom();
                    Console.ReadKey();
                    break;

                case "View Rooms":
                    roomController.ViewRooms();
                    Console.ReadKey();
                    break;

                case "Delete Room":
                    roomController.DeleteRoom();
                    Console.ReadKey();
                    break;

                case "Add Customer":
                    guestController.AddGuest();
                    Console.ReadKey();
                    break;

                case "Update Customer":
                    guestController.UpdateGuest();
                    Console.ReadKey();
                    break;

                case "View Customers":
                    guestController.ViewGuests();
                    Console.ReadKey();
                    break;

                case "Delete Customer":
                    guestController.DeleteGuest();
                    Console.ReadKey();
                    break;

                case "Add Booking":
                    bookingController.AddBooking();
                    Console.ReadKey();
                    break;


                case "View Bookings":
                    bookingController.ViewBookings();
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
