using System;
using Spectre.Console;
using System.Text.Json;
using FluentValidation;
using System.Text.RegularExpressions;
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
                AnsiConsole.Write(new FigletText("Vehicle. Workshop. System ")
                    .Centered()
                    .Color(Color.Yellow)); 
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
                    int id;
                    string name, phone, email, plate, address;

                    // Loop tills ett giltigt ID anges
                    while (true)
                    {
                        Console.Write("Enter Customer ID: ");
                        if (int.TryParse(Console.ReadLine(), out id) && id > 0)
                            break;
                        AnsiConsole.Markup("[red]Invalid ID. It must be a positive number.[/]");
                    }

                    // Loop tills ett giltigt namn anges
                    while (true)
                    {
                        Console.Write("Enter Name: ");
                        name = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(name))
                            break;
                        AnsiConsole.Markup("[red]Name cannot be empty.[/]");
                    }

                    // Loop tills ett giltigt telefonnummer anges
                      bool isValid = false;

                    while (true) // Fortsätt tills giltig inmatning ges
                    {
                        Console.Write("Enter phone number (format: +46XXXXXXXXX or 0XXXXXXXXX): ");
                        phone = Console.ReadLine();

                        // Regex för svenska telefonnummer
                        string pattern = @"^(\+46|0)\d{9}$";

                        if (Regex.IsMatch(phone, pattern))
                        {
                            Console.WriteLine($"Phone number {phone} is valid.");
                            isValid = true; // Avsluta loopen
                            break;
                        }
                            
                        else
                        {
                            AnsiConsole.Markup("[Red]Invalid phone number. Please try again.[/]");
                        }
                    }

                    // Fortsätt med resten av programmet
                    AnsiConsole.Markup("[blue]Thank you! Moving forward.[/]");
                

                    // Loop tills en giltig e-post anges
                    while (true)
                    {
                        Console.Write("Enter Email: ");
                        email = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(email) && email.Contains("@"))
                            break;
                        AnsiConsole.Markup("[red]Invalid email address. Please include '@'.[/]");
                    }

                    // Loop tills ett giltigt registreringsnummer anges
                    while (true)
                    {
                        Console.Write("Enter Vehicle Plate Number: ");
                        plate = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(plate))
                            break;
                        AnsiConsole.Markup("[red]Vehicle plate number cannot be empty.[/]");
                    }

                    // Loop tills en giltig faktureringsadress anges
                    while (true)
                    {
                        Console.Write("Enter Billing Address: ");
                        address = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(address))
                            break;
                        AnsiConsole.Markup("[red]Billing address cannot be empty.[/]");
                    }

                    // Skapa och lägg till kund efter all validering
                    try
                    {
                        Customer newCustomer = new Customer(id, name, phone, email, plate, address);
                        customerManager.AddCustomer(newCustomer);

                        AnsiConsole.Markup("\n[green]Customer added successfully![/]");
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.Markup($"[red]An error occurred: {ex.Message}[/]");
                    }

                    Console.ReadKey();
                    break;


                case 1: // View All Customers
                    customerManager.ViewAllCustomers();
                    AnsiConsole.Markup("[yellow]\nPress any key to return to the menu.[/]");
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
