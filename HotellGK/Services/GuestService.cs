using HotellGK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellGK.Services
{
    public class GuestService
    {
        private readonly HotelDbContext _context;

        public GuestService(HotelDbContext context)
        {
            _context = context;
        }

        public void Add(Guest entity)
        {
            _context.Guests.Add(entity);
            _context.SaveChanges();
            Console.WriteLine("Guest added successfully!");
        }
    }
}
