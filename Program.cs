using System;

namespace VehicleWorkshopManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager();
            string[] menuOptions = {
                "Add Customer",
                "View All Customers",
                "Update Customer",
                "Delete Customer",
                "Register Complaint",
                "View Complaints",
                "Generate Invoice",
                "Exit"
            };

            int selectedIndex = 0;
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("--- Vehicle Workshop Customer Management ---\n");
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"> {menuOptions[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {menuOptions[i]}");
                    }
                }

                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex == 0) ? menuOptions.Length - 1 : selectedIndex - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex == menuOptions.Length - 1) ? 0 : selectedIndex + 1;
                        break;

                    case ConsoleKey.Enter:
                        HandleMenuSelection(selectedIndex, customerManager, ref exit);
                        break;
                }
            }
        }

        private static void HandleMenuSelection(int selectedIndex, CustomerManager customerManager, ref bool exit)
        {
            Console.Clear();
            switch (selectedIndex)
            {
                case 0: // Add Customer
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

                    // Pass all required arguments to the constructor
                    Customer newCustomer = new Customer(id, name, phone, email, plate, address);
                    customerManager.AddCustomer(newCustomer);

                    Console.WriteLine("\nCustomer added successfully!");
                    Console.ReadKey();
                    break;


                case 1: // View All Customers
                    customerManager.ViewAllCustomers();
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                    break;

                case 2: // Update Customer
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
                    Console.WriteLine("\nCustomer updated successfully!");
                    Console.ReadKey();
                    break;

                case 3: // Delete Customer
                    Console.Write("Enter Customer ID to delete: ");
                    int deleteId = int.Parse(Console.ReadLine());
                    customerManager.DeleteCustomer(deleteId);
                    Console.WriteLine("\nCustomer deleted successfully!");
                    Console.ReadKey();
                    break;

                case 4: // Register Complaint
                    Console.Write("Enter Customer ID: ");
                    int complaintCustomerId = int.Parse(Console.ReadLine());
                    Console.Write("Enter Complaint Description: ");
                    string complaintDescription = Console.ReadLine();
                    Console.Write("Enter Cause: ");
                    string cause = Console.ReadLine();
                    Console.Write("Enter Correction: ");
                    string correction = Console.ReadLine();
                    customerManager.RegisterComplaint(complaintCustomerId, complaintDescription, cause, correction);
                    Console.ReadKey();
                    break;

                case 5: // View Complaints
                    Console.Write("Enter Customer ID: ");
                    int viewComplaintCustomerId = int.Parse(Console.ReadLine());
                    customerManager.ViewComplaints(viewComplaintCustomerId);
                    Console.ReadKey();
                    break;

                case 6: // Generate Invoice
                    Console.Write("Enter Customer ID: ");
                    int invoiceCustomerId = int.Parse(Console.ReadLine());
                    Console.Write("Enter Service Cost: ");
                    decimal serviceCost = decimal.Parse(Console.ReadLine());
                    Console.Write("Enter Material Cost: ");
                    decimal materialCost = decimal.Parse(Console.ReadLine());
                    customerManager.GenerateInvoice(invoiceCustomerId, serviceCost, materialCost);
                    Console.ReadKey();
                    break;
                
                case 7://Exit
                    Console.WriteLine("Exiting the application... Goodbye!");
                    Environment.Exit(0); // Terminates the application
                    break;
            }
        }
    }
}
