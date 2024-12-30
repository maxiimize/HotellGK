using Microsoft.Extensions.DependencyInjection;

namespace HotellGK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new HotelDbContext())
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
