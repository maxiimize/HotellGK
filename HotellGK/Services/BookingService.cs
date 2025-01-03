using HotellGK.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotellGK.MenuClasses;

namespace HotellGK.Services
{
    public class BookingService : IService<Booking>
    {
        private readonly HotelDbContext _context;

        public BookingService(HotelDbContext context)
        {
            _context = context;
        }

        public void Add(Booking entity)
        {
            if (!IsRoomAvailable(entity.RoomId, entity.CheckInDate, entity.CheckOutDate))
            {
                Console.WriteLine("Room is not available for the selected dates.");
                return;
            }

            _context.Bookings.Add(entity);
            _context.SaveChanges();
        }

        public List<Booking> GetAll()
        {
            return _context.Bookings.Include(b => b.Room).Include(b => b.Guest).ToList();
        }

        public void Update(int id, Booking entity)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == id);
            if (booking == null)
            {
                Console.WriteLine("Booking not found.");
                return;
            }

            booking.CheckInDate = entity.CheckInDate;
            booking.CheckOutDate = entity.CheckOutDate;
            booking.RoomId = entity.RoomId;
            booking.GuestId = entity.GuestId;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == id);
            if (booking == null)
            {
                Console.WriteLine("Booking not found.");
                return;
            }

            _context.Bookings.Remove(booking);
            _context.SaveChanges();
        }

        public bool IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            return !_context.Bookings.Any(b => b.RoomId == roomId &&
                                               ((checkInDate >= b.CheckInDate && checkInDate < b.CheckOutDate) ||
                                                (checkOutDate > b.CheckInDate && checkOutDate <= b.CheckOutDate) ||
                                                (checkInDate <= b.CheckInDate && checkOutDate >= b.CheckOutDate)));
        }
        public List<Room> GetAllAvailableRooms()
        {
            var bookedRoomIds = _context.Bookings
                .Where(b => b.CheckOutDate >= DateTime.Now)
                .Select(b => b.RoomId)
                .ToHashSet();

            return _context.Rooms
                .Where(r => !bookedRoomIds.Contains(r.RoomId))
                .ToList();
        }

    }
}
