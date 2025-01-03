using HotellGK.Models;
using HotellGK.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellGK.Controllers
{
    public class GuestController
    {
        private readonly GuestService _guestService;

        public GuestController(GuestService guestService)
        {
            _guestService = guestService;
        }

        public void AddGuest()
        {
            Console.Write("Enter Customer Name: ");
            var customerName = Console.ReadLine();
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
            Console.WriteLine("Customer added successfully!");
        }

        public void ViewGuests()
        {
            var customers = _guestService.GetAll();
            foreach (var customer in customers)
            {
                Console.WriteLine($"" +
                    $"ID: {customer.GuestId}, " +
                    $"Name: {customer.Name}, " +
                    $"Email: {customer.Email}, " +
                    $"Phone: {customer.PhoneNumber}");
            }
        }
        public void UpdateGuest()
        {
            Console.Write("Enter Customer ID to update: ");
            var customerIdToUpdate = int.Parse(Console.ReadLine());

            var customer = _guestService.GetAll().FirstOrDefault(g => g.GuestId == customerIdToUpdate);
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            Console.Write($"Enter New Name (current: {customer.Name}): ");
            var newName = Console.ReadLine();
            Console.Write($"Enter New Email (current: {customer.Email}): ");
            var newEmail = Console.ReadLine();
            Console.Write($"Enter New Phone Number (current: {customer.PhoneNumber}): ");
            var newPhone = Console.ReadLine();

            _guestService.Update(customerIdToUpdate, new Guest
            {
                Name = string.IsNullOrWhiteSpace(newName) ? customer.Name : newName,
                Email = string.IsNullOrWhiteSpace(newEmail) ? customer.Email : newEmail,
                PhoneNumber = string.IsNullOrWhiteSpace(newPhone) ? customer.PhoneNumber : newPhone
            });

            Console.WriteLine("Customer updated successfully!");
        }

        public void DeleteGuest()
        {
            Console.Write("Enter Customer ID to delete: ");
            var customerIdToDelete = int.Parse(Console.ReadLine());

            var customer = _guestService.GetAll().FirstOrDefault(g => g.GuestId == customerIdToDelete);
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            _guestService.Delete(customerIdToDelete);
            Console.WriteLine("Customer deleted successfully!");
        }
    }
}
