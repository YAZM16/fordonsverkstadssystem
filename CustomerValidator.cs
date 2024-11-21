using FluentValidation;

namespace VehicleWorkshopManagement
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(c => c.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Phone number must be valid.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(c => c.VehiclePlateNumber)
                .NotEmpty().WithMessage("Vehicle plate number is required.");

            RuleFor(c => c.BillingAddress)
                .NotEmpty().WithMessage("Billing address is required.");
        }
    }
}
