using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using HotellGK.Models;
using Microsoft.Extensions.DependencyInjection;

namespace HotellGK
{
    public class HotelDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasData(
                new Room { RoomId = 1, RoomType = "Single", HasExtraBeds = false, MaxExtraBeds = 0, IsAvailable = true },
                new Room { RoomId = 2, RoomType = "Double", HasExtraBeds = true, MaxExtraBeds = 2, IsAvailable = true },
                new Room { RoomId = 3, RoomType = "Double", HasExtraBeds = true, MaxExtraBeds = 1, IsAvailable = true },
                new Room { RoomId = 4, RoomType = "Single", HasExtraBeds = false, MaxExtraBeds = 0, IsAvailable = true }
            );

            modelBuilder.Entity<Guest>().HasData(
                new Guest { GuestId = 1, Name = "John Doe", Email = "john@example.com" },
                new Guest { GuestId = 2, Name = "Jane Smith", Email = "jane@example.com" },
                new Guest { GuestId = 3, Name = "Mike Brown", Email = "mike@example.com" },
                new Guest { GuestId = 4, Name = "Lisa White", Email = "lisa@example.com" }
            );
        }
    }

}
