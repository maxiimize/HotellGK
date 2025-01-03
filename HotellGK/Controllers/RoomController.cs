using HotellGK.MenuClasses;
using HotellGK.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellGK.Controllers
{
    public class RoomController
    {
        private readonly RoomService _roomService;
        private readonly Menu _menu;

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
            _menu = new Menu();
        }

        public void AddRoom()
        {
            string roomType = _menu.DrawMenuController(new[] { "Single", "Double" }, "Select Room Type:");
            string roomSize = _menu.DrawMenuController(new[] { "Normal", "Large" }, "Select Room Size:");


            bool hasExtraBeds = false;
            int maxExtraBeds = 0;


            if (roomType.Equals("Double", StringComparison.OrdinalIgnoreCase))
            {
                hasExtraBeds = _menu.DrawMenuController(new[] { "Yes", "No" }, "Has Extra Beds?") == "Yes";

                if (hasExtraBeds)
                {
                    if (roomSize.Equals("Normal", StringComparison.OrdinalIgnoreCase))
                    {
                        string maxBeds = _menu.DrawMenuController(new[] { "0", "1" }, "Select Max Extra Beds (0-1):");
                        maxExtraBeds = int.Parse(maxBeds);
                    }
                    else if (roomSize.Equals("Large", StringComparison.OrdinalIgnoreCase))
                    {
                        string maxBeds = _menu.DrawMenuController(new[] { "0", "1", "2" }, "Select Max Extra Beds (0-2):");
                        maxExtraBeds = int.Parse(maxBeds);
                    }
                }
            }
            else if (roomType.Equals("Single", StringComparison.OrdinalIgnoreCase))
            {
                hasExtraBeds = false;
                maxExtraBeds = 0;
            }

            _roomService.Add(new Room
            {
                RoomType = roomType,
                RoomSize = roomSize,
                HasExtraBeds = hasExtraBeds,
                MaxExtraBeds = maxExtraBeds,
                IsAvailable = true
            });

            Console.WriteLine("Press any key to continue. . .");

        }

        public void ViewRooms()
        {
            var rooms = _roomService.GetAll();

            if (!rooms.Any())
            {
                Console.WriteLine("No rooms available.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine(new string('=', 80));
            Console.WriteLine($"{"ID",-5}{"Room Type",-15}{"Size",-10}{"Extra Beds",-12}{"Max Extra Beds",-15}{"Available",-10}");
            Console.WriteLine(new string('=', 80));

            foreach (var room in rooms)
            {
                Console.WriteLine($"{room.RoomId,-5}{room.RoomType,-15}{room.RoomSize,-10}{(room.HasExtraBeds ? "Yes" : "No"),-12}{room.MaxExtraBeds,-15}{(room.IsAvailable ? "Yes" : "No"),-10}");
            }

            Console.WriteLine(new string('=', 80));
            Console.WriteLine("Press any key to return to the menu...");

        }

        public void UpdateRoom()
        {
            var rooms = _roomService.GetAll();

            if (!rooms.Any())
            {
                Console.WriteLine("No rooms available to update.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            var roomOptions = rooms.Select(r => $"{r.RoomId}: {r.RoomType}, {r.RoomSize}, Extra Beds: {(r.HasExtraBeds ? "Yes" : "No")}, Available: {(r.IsAvailable ? "Yes" : "No")}").ToArray();
            string selectedRoom = _menu.DrawMenuController(roomOptions, "Select a room to update:");
            int roomIdToUpdate = int.Parse(selectedRoom.Split(':')[0]);

            var existingRoom = rooms.FirstOrDefault(r => r.RoomId == roomIdToUpdate);
            if (existingRoom == null)
            {
                Console.WriteLine("Room not found.");
                Console.ReadKey();
                return;
            }

            string newRoomType = _menu.DrawMenuController(new[] { "Single", "Double" }, $"Select New Room Type (current: {existingRoom.RoomType}):");

            string newRoomSize = _menu.DrawMenuController(new[] { "Normal", "Large" }, $"Select New Room Size (current: {existingRoom.RoomSize}):");

            bool newHasExtraBeds = false;
            int newMaxExtraBeds = 0;

            if (newRoomType.Equals("Double", StringComparison.OrdinalIgnoreCase))
            {
                newHasExtraBeds = _menu.DrawMenuController(new[] { "Yes", "No" }, $"Has Extra Beds? (current: {(existingRoom.HasExtraBeds ? "Yes" : "No")})") == "Yes";

                if (newHasExtraBeds)
                {
                    if (newRoomSize.Equals("Normal", StringComparison.OrdinalIgnoreCase))
                    {
                        string maxBeds = _menu.DrawMenuController(new[] { "0", "1" }, $"Select Max Extra Beds (0-1) (current: {existingRoom.MaxExtraBeds}):");
                        newMaxExtraBeds = int.Parse(maxBeds);
                    }
                    else if (newRoomSize.Equals("Large", StringComparison.OrdinalIgnoreCase))
                    {
                        string maxBeds = _menu.DrawMenuController(new[] { "0", "1", "2" }, $"Select Max Extra Beds (0-2) (current: {existingRoom.MaxExtraBeds}):");
                        newMaxExtraBeds = int.Parse(maxBeds);
                    }
                }
            }
            else if (newRoomType.Equals("Single", StringComparison.OrdinalIgnoreCase))
            {
                newHasExtraBeds = false;
                newMaxExtraBeds = 0;
            }

            _roomService.Update(roomIdToUpdate, new Room
            {
                RoomType = newRoomType,
                RoomSize = newRoomSize,
                HasExtraBeds = newHasExtraBeds,
                MaxExtraBeds = newMaxExtraBeds,
                IsAvailable = existingRoom.IsAvailable
            });

            Console.WriteLine("Room updated successfully! Press any key to continue...");
            Console.ReadKey();
        }




        public void DeleteRoom()
        {
            var rooms = _roomService.GetAll();

            if (!rooms.Any())
            {
                Console.WriteLine("No rooms available to delete.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("Select a room to delete:\n");

            
            var roomOptions = rooms.Select(r => $"{r.RoomId}: {r.RoomType}, {r.RoomSize}, Extra Beds: {(r.HasExtraBeds ? "Yes" : "No")}, Available: {(r.IsAvailable ? "Yes" : "No")}").ToArray();
            string selectedRoom = _menu.DrawMenuController(roomOptions, "Available Rooms:");

            
            int roomIdToDelete = int.Parse(selectedRoom.Split(':')[0]);

            try
            {
                _roomService.Delete(roomIdToDelete);
                Console.WriteLine($"Room ID {roomIdToDelete} deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Can't delete Room ID {roomIdToDelete}. There are active bookings connected to it.");
            }

            Console.WriteLine("Press any key to continue. . .");
        }
    }
}
