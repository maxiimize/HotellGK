using HotellGK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellGK.Services
{
    internal class RoomService : IService<Room>
    {
        private readonly HotelDbContext Context;

        public RoomService(HotelDbContext context)
        {
            Context = context;
        }

        public void Add(Room entity)
        {
            Context.Rooms.Add(entity);
            Context.SaveChanges();
            Console.WriteLine("Room added successfully!");
        }
    }
}
