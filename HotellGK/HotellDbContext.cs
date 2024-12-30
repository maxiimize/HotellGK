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
                new Room { Id = 1, RoomType = "Single", HasExtraBeds = false },
                new Room { Id = 2, RoomType = "Double", HasExtraBeds = true },
                new Room { Id = 3, RoomType = "Double", HasExtraBeds = true },
                new Room { Id = 4, RoomType = "Single", HasExtraBeds = false }
            );

            modelBuilder.Entity<Guest>().HasData(
                new Guest { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new Guest { Id = 2, Name = "Jane Smith", Email = "jane@example.com" },
                new Guest { Id = 3, Name = "Mike Brown", Email = "mike@example.com" },
                new Guest { Id = 4, Name = "Lisa White", Email = "lisa@example.com" }
            );
        }
    }

}
