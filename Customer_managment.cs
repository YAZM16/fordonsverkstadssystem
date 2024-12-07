using System;
using System.Collections.Generic;
using System.IO;
using FluentValidation;
using Newtonsoft.Json;
using System.Text.Json;

namespace VehicleWorkshopManagement
{
    public class CustomerManager
    {
        private const string FilePath = @"C:\Users\user\source\repos\fordonsverkstadssystem_Åtgärder\customers_Data.json";
        private List<Customer> customers = new List<Customer>();

        private CustomerValidator validator = new CustomerValidator();

        public CustomerManager()
        {
            LoadData(); // Load data on initialization
        }

        public void AddCustomer(Customer customer)
        {
            var validationResult = validator.Validate(customer);
            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    Console.WriteLine($"Validation failed for {failure.PropertyName}: {failure.ErrorMessage}");
                }
                return; // Stop if validation fails
            }

            customers.Add(customer);
            SaveData();
            
        }

        public void ViewAllCustomers()
        {
            if (customers.Count == 0)
            {
                Console.WriteLine("No customers found.");
                return;
            }

            Console.WriteLine("Customer List:");
            foreach (var customer in customers)
            {
                customer.DisplayCustomerDetails();
            }
        }

        public void UpdateCustomer(int customerId, string name, string phoneNumber, string email, string vehiclePlateNumber, string billingAddress)
        {
            var customer = customers.Find(c => c.CustomerId == customerId);
            if (customer != null)
            {
                customer.Name = name;
                customer.PhoneNumber = phoneNumber;
                customer.Email = email;
                customer.VehiclePlateNumber = vehiclePlateNumber;
                customer.BillingAddress = billingAddress;

                var validationResult = validator.Validate(customer);
                if (!validationResult.IsValid)
                {
                    foreach (var failure in validationResult.Errors)
                    {
                        Console.WriteLine($"Validation failed for {failure.PropertyName}: {failure.ErrorMessage}");
                    }
                    return; // Stop if validation fails
                }

               
                Console.WriteLine("Customer updated successfully!");
                SaveData();
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }

        public void DeleteCustomer(int customerId)
        {
            var customer = customers.Find(c => c.CustomerId == customerId);
            if (customer != null)
            {
                customers.Remove(customer);
                SaveCustomers();
                Console.WriteLine("Customer deleted successfully!");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }

        private void LoadCustomers()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    string jsonData = File.ReadAllText(FilePath);
                    customers = JsonConvert.DeserializeObject<List<Customer>>(jsonData) ?? new List<Customer>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading data: {ex.Message}");
                }
            }
        }

        private void SaveCustomers()
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(customers, Formatting.Indented);
                File.WriteAllText(FilePath, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }
        public void RegisterComplaint(int customerId, string description, string cause, string correction)
        {
            Customer customer = customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer != null)
            {
                customer.Complaints.Add(new Complaint
                {
                    ComplaintDescription = description,
                    Cause = cause,
                    Correction = correction,
                    DateReported = DateTime.Now
                });
                SaveData();
                Console.WriteLine("Complaint registered successfully.");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }
        public void ViewComplaints(int customerId)
        {
            Customer customer = customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer != null && customer.Complaints.Any())
            {
                Console.WriteLine($"Complaints for {customer.Name}:");
                foreach (var complaint in customer.Complaints)
                {
                    Console.WriteLine($"- Description: {complaint.ComplaintDescription}");
                    Console.WriteLine($"  Cause: {complaint.Cause}");
                    Console.WriteLine($"  Correction: {complaint.Correction}");
                    Console.WriteLine($"  Date Reported: {complaint.DateReported}\n");
                }
            }
            else
            {
                Console.WriteLine("No complaints found for this customer.");
            }
        }
        public void GenerateInvoice(int customerId, decimal serviceCost, decimal materialCost)
        {
            const decimal VATRate = 0.25m; // 25% VAT
            decimal subtotal = serviceCost + materialCost;
            decimal vat = subtotal * VATRate;
            decimal total = subtotal + vat;

            Console.WriteLine("\n--- Invoice ---");
            Console.WriteLine($"Customer ID: {customerId}");
            Console.WriteLine($"Service Cost: {serviceCost:C}");
            Console.WriteLine($"Material Cost: {materialCost:C}");
            Console.WriteLine($"Subtotal: {subtotal:C}");
            Console.WriteLine($"VAT (25%): {vat:C}");
            Console.WriteLine($"Total: {total:C}");
        }
        private void SaveData()
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(customers, Formatting.Indented);
                File.WriteAllText(FilePath, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }

        private void LoadData()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    var jsonData = File.ReadAllText(FilePath);
                    customers = JsonConvert.DeserializeObject<List<Customer>>(jsonData) ?? new List<Customer>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
                customers = new List<Customer>(); // Initialize with an empty list on failure
            }
        }
    }
}
