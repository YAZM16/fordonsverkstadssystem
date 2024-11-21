using System;

namespace VehicleWorkshopManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager();
            int choice;

            do
            {
                Console.WriteLine("\n--- Vehicle Workshop Customer Management ---");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. View All Customers");
                Console.WriteLine("3. Update Customer");
                Console.WriteLine("4. Delete Customer");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter Customer ID: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Enter Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter Phone Number: ");
                        string phone = Console.ReadLine();
                        Console.Write("Enter Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Enter Vehicle Plate Number: ");
                        string plate = Console.ReadLine();
                        Console.Write("Enter Billing Address: ");
                        string address = Console.ReadLine();

                        Customer newCustomer = new Customer(id, name, phone, email, plate, address);
                        customerManager.AddCustomer(newCustomer);
                        break;

                    case 2:
                        customerManager.ViewAllCustomers();
                        break;

                    case 3:
                        Console.Write("Enter Customer ID to update: ");
                        int updateId = int.Parse(Console.ReadLine());
                        Console.Write("Enter New Name: ");
                        string newName = Console.ReadLine();
                        Console.Write("Enter New Phone Number: ");
                        string newPhone = Console.ReadLine();
                        Console.Write("Enter New Email: ");
                        string newEmail = Console.ReadLine();
                        Console.Write("Enter New Vehicle Plate Number: ");
                        string newPlate = Console.ReadLine();
                        Console.Write("Enter New Billing Address: ");
                        string newAddress = Console.ReadLine();

                        customerManager.UpdateCustomer(updateId, newName, newPhone, newEmail, newPlate, newAddress);
                        break;

                    case 4:
                        Console.Write("Enter Customer ID to delete: ");
                        int deleteId = int.Parse(Console.ReadLine());
                        customerManager.DeleteCustomer(deleteId);
                        break;

                    case 5:
                        Console.WriteLine("Exiting... Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            } while (choice != 5);
        }
    }
}
