using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace HotellGK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            
            var services = new ServiceCollection();
            services.AddSingleton(configuration);
            services.AddDbContext<HotelDbContext>();

            var serviceProvider = services.BuildServiceProvider();

            
            using (var context = serviceProvider.GetService<HotelDbContext>())
            {
                context.Database.EnsureCreated();
            }

            
            string[] menuOptions =
            {
                "Add Room",
                "Add Customer",
                "Add Booking",
                "View Rooms",
                "View Customers",
                "View Bookings",
                "Exit"
            };

            int selectedOption = 0;

            while (true)
            {
                Console.Clear();
                Menu.DrawMenu(menuOptions, selectedOption);

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOption = (selectedOption == 0) ? menuOptions.Length - 1 : selectedOption - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOption = (selectedOption == menuOptions.Length - 1) ? 0 : selectedOption + 1;
                        break;
                    case ConsoleKey.Enter:
                        Menu.HandleMenuSelection(menuOptions[selectedOption]);
                        break;
                }
            }
        }
    }
}
