using HotellGK.Models;
using HotellGK.Services;
using HotellGK.MenuClasses;
using System;
using System.Linq;

namespace HotellGK.Controllers
{
    public class GuestController
    {
        private readonly GuestService _guestService;
        private readonly Menu _menu;

        public GuestController(GuestService guestService)
        {
            _guestService = guestService;
            _menu = new Menu();
        }

        public void AddGuest()
        {
            string customerName;
            do
            {
                Console.Write("Enter Customer Name: ");
                customerName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(customerName))
                {
                    Console.WriteLine("Customer name cannot be empty. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(customerName));

            Console.Write("Enter Customer Email: ");
            var customerEmail = Console.ReadLine();
            Console.Write("Enter Customer Phone Number: ");
            var customerPhone = Console.ReadLine();

            _guestService.Add(new Guest
            {
                Name = customerName,
                Email = customerEmail,
                PhoneNumber = customerPhone
            });

            Console.WriteLine("Customer added successfully! Press any key to continue...");
            Console.ReadKey();
        }


        public void ViewGuests()
        {
            var customers = _guestService.GetAll();

            if (!customers.Any())
            {
                Console.WriteLine("No customers available.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"{"ID",-5}{"Name",-15}{"Email",-20}{"Phone",-10}");
            Console.WriteLine(new string('=', 50));

            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.GuestId,-5}{customer.Name,-15}{customer.Email,-20}{customer.PhoneNumber,-10}");
            }

            Console.WriteLine(new string('=', 50));
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        public void UpdateGuest()
        {
            var customers = _guestService.GetAll();

            if (!customers.Any())
            {
                Console.WriteLine("No customers available to update.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            var customerOptions = customers.Select(c => $"{c.GuestId}: {c.Name}, {c.Email}, {c.PhoneNumber}").ToArray();
            string selectedCustomer = _menu.DrawMenuController(customerOptions, "Select a customer to update:");

            int customerIdToUpdate = int.Parse(selectedCustomer.Split(':')[0]);
            var customer = customers.FirstOrDefault(g => g.GuestId == customerIdToUpdate);

            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Current Name: {customer.Name}");
            if (_menu.DrawMenuController(new[] { "Yes", "No" }, "Do you want to update the name?") == "Yes")
            {
                Console.Write("Enter New Name: ");
                customer.Name = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(customer.Name))
                {
                    Console.WriteLine("Name cannot be empty. Please enter a valid name.");
                    Console.Write("Enter New Name: ");
                    customer.Name = Console.ReadLine();
                }
            }

            Console.WriteLine($"Current Email: {customer.Email}");
            if (_menu.DrawMenuController(new[] { "Yes", "No" }, "Do you want to update the email?") == "Yes")
            {
                Console.Write("Enter New Email: ");
                customer.Email = Console.ReadLine();
            }

            Console.WriteLine($"Current Phone Number: {customer.PhoneNumber}");
            if (_menu.DrawMenuController(new[] { "Yes", "No" }, "Do you want to update the phone number?") == "Yes")
            {
                Console.Write("Enter New Phone Number: ");
                customer.PhoneNumber = Console.ReadLine();
            }

            _guestService.Update(customerIdToUpdate, customer);

            Console.WriteLine("Customer updated successfully! Press any key to continue...");
            Console.ReadKey();
        }



        public void DeleteGuest()
        {
            var customers = _guestService.GetAll();

            if (!customers.Any())
            {
                Console.WriteLine("No customers available to delete.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            var customerOptions = customers.Select(c => $"{c.GuestId}: {c.Name}, {c.Email}, {c.PhoneNumber}").ToArray();
            string selectedCustomer = _menu.DrawMenuController(customerOptions, "Select a customer to delete:");

            int customerIdToDelete = int.Parse(selectedCustomer.Split(':')[0]);

            try
            {
                _guestService.Delete(customerIdToDelete);
                Console.WriteLine($"Customer ID {customerIdToDelete} deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete Customer ID {customerIdToDelete}. Reason: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
