namespace VehicleWorkshopManagement
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string VehiclePlateNumber { get; set; }
        public string BillingAddress { get; set; }

        public Customer(int customerId, string name, string phoneNumber, string email, string vehiclePlateNumber, string billingAddress)
        {
            CustomerId = customerId;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            VehiclePlateNumber = vehiclePlateNumber;
            BillingAddress = billingAddress;
        }

        public void DisplayCustomerDetails()
        {
            Console.WriteLine($"ID: {CustomerId}, Name: {Name}, Phone: {PhoneNumber}, Email: {Email}, Plate: {VehiclePlateNumber}, Address: {BillingAddress}");
        }
    }
}
