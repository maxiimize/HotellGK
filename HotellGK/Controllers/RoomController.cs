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
        private readonly Menu _menu; // Lägg till ett fält för att använda Menu-klassen

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
            _menu = new Menu(); // Skapa en ny instans av Menu
        }

        public void AddRoom()
        {
            string roomType = _menu.DrawMenuController(new[] { "Single", "Double" }, "Select Room Type:");
            string roomSize = _menu.DrawMenuController(new[] { "Normal", "Large" }, "Select Room Size:");

            // Standardvärde för extra sängar
            bool hasExtraBeds = false;
            int maxExtraBeds = 0;

            // Hantera extra sängar endast om rummet är Double
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
        }

        public void UpdateRoom()
        {
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
                            return;
                        }
                    }
                    else if (newRoomSize.Equals("Large", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.Write("Enter Max Extra Beds (0-2): ");
                        newMaxExtraBeds = int.Parse(Console.ReadLine());
                        if (newMaxExtraBeds < 0 || newMaxExtraBeds > 2)
                        {
                            Console.WriteLine("Invalid number of extra beds for a Large Double room. Must be 0, 1, or 2.");
                            return;
                        }
                    }
                }
            }

            _roomService.Update(roomIdToUpdate, new Room
            {
                RoomType = newRoomType,
                RoomSize = newRoomSize,
                HasExtraBeds = newHasExtraBeds,
                MaxExtraBeds = newMaxExtraBeds,
                IsAvailable = true
            });

            Console.WriteLine("Room updated successfully!");
        }

        public void DeleteRoom()
        {
            Console.Write("Enter Room ID to delete: ");
            var roomIdToDelete = int.Parse(Console.ReadLine());

            var room = _roomService.GetAll().FirstOrDefault(r => r.RoomId == roomIdToDelete);
            if (room == null)
            {
                Console.WriteLine("Room not found.");
                return;
            }

            _roomService.Delete(roomIdToDelete);
            Console.WriteLine("Room deleted successfully!");
        }
    }
}
