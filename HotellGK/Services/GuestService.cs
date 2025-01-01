using HotellGK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellGK.Services
{
    public class GuestService : IService<Guest>
    {
        private readonly HotelDbContext _context;

        public GuestService(HotelDbContext context)
        {
            _context = context;
        }

        public List<Guest> GetAll()
        {
            return _context.Guests.ToList();
        }

        public void Add(Guest entity)
        {
            _context.Guests.Add(entity);
            _context.SaveChanges();
            Console.WriteLine("Guest added successfully!");
        }

        public void Update(int id, Guest entity)
        {
            var guest = _context.Guests.FirstOrDefault(g => g.GuestId == id);
            if (guest == null)
            {
                Console.WriteLine("Guest not found.");
                return;
            }

            guest.Name = entity.Name;
            guest.Email = entity.Email;
            guest.PhoneNumber = entity.PhoneNumber;
            _context.SaveChanges();
            Console.WriteLine("Guest updated successfully!");
        }

        public void Delete(int id)
        {
            var guest = _context.Guests.FirstOrDefault(g => g.GuestId == id);
            if (guest == null)
            {
                Console.WriteLine("Guest not found.");
                return;
            }

            _context.Guests.Remove(guest);
            _context.SaveChanges();
            Console.WriteLine("Guest deleted successfully!");
        }
    }
}
