using HotellGK.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _context.Bookings.Add(entity);
            _context.SaveChanges();
            Console.WriteLine("Booking added successfully!");
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
            Console.WriteLine("Booking updated successfully!");
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
            Console.WriteLine("Booking deleted successfully!");
        }
    }
}
