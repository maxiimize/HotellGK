using HotellGK.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellGK.Services
{
    public class RoomService : IService<Room>
    {
        private readonly HotelDbContext _context;

        public RoomService(HotelDbContext context)
        {
            _context = context;
        }

        public void Add(Room entity)
        {
            if (_context.Rooms.Count() >= 15)
            {
                Console.WriteLine("Cannot add more rooms. The maximum limit of 15 rooms has been reached.");
                return;
            }

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
        }


        public void Delete(int id)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == id);
            if (room == null)
            {
                throw new Exception($"Room with ID {id} not found.");
            }

            
            var hasAnyBookings = _context.Bookings.Any(b => b.RoomId == id);

            if (hasAnyBookings)
            {
                throw new Exception($"Cannot delete Room ID {id}. It has associated bookings.");
            }

            _context.Rooms.Remove(room);
            _context.SaveChanges();
        }




        public List<(Room Room, string Status)> GetRoomStatuses()
        {
            var rooms = _context.Rooms.ToList();
            var bookings = _context.Bookings.ToList();

            var roomStatuses = rooms.Select(room =>
            {
                var isOccupied = bookings.Any(b =>
                    b.RoomId == room.RoomId &&
                    b.CheckInDate.Date <= DateTime.Now.Date &&
                    b.CheckOutDate.Date > DateTime.Now.Date);

                return (Room: room, Status: isOccupied ? "Occupied" : "Available");
            }).ToList();

            return roomStatuses;
        }


    }
}
