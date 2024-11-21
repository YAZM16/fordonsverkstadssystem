using System;
using System.Collections.Generic;
using System.IO;
using FluentValidation;
using Newtonsoft.Json;

namespace VehicleWorkshopManagement
{
    public class CustomerManager
    {
        private const string FilePath = "customers.json";
        private List<Customer> customers = new List<Customer>();
        private CustomerValidator validator = new CustomerValidator();

        public CustomerManager()
        {
            LoadCustomers(); // Load data on initialization
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
            SaveCustomers();
            Console.WriteLine("Customer added successfully!");
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

                SaveCustomers();
                Console.WriteLine("Customer updated successfully!");
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
    }
}
