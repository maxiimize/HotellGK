using HotellGK.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellGK.Services
{
    internal class RoomService : IService<Room>
    {
        private readonly HotelDbContext _context;

        public RoomService(HotelDbContext context)
        {
            _context = context;
        }

        public void Add(Room entity)
        {
            _context.Rooms.Add(entity);
            _context.SaveChanges();
            Console.WriteLine("Room added successfully!");
        }

        public List<Room> GetAll()
        {
            return _context.Rooms.ToList();
        }

        public void Update(int id, Room entity)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == id);
            if (room == null)
            {
                Console.WriteLine("Room not found.");
                return;
            }

            room.RoomType = entity.RoomType;
            room.HasExtraBeds = entity.HasExtraBeds;
            room.MaxExtraBeds = entity.MaxExtraBeds;
            room.IsAvailable = entity.IsAvailable;

            _context.SaveChanges();
            Console.WriteLine("Room updated successfully!");
        }


        public void Delete(int id)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == id);
            if (room == null)
            {
                Console.WriteLine("Room not found.");
                return;
            }

            var hasActiveBookings = _context.Bookings.Any(b => b.RoomId == id);
            if (hasActiveBookings)
            {
                Console.WriteLine("Cannot delete room. It has active bookings.");
                return;
            }

            _context.Rooms.Remove(room);
            _context.SaveChanges();
            Console.WriteLine("Room deleted successfully!");
        }
    }
}
