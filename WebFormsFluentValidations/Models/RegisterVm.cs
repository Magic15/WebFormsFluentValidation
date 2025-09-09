using FluentValidation;

namespace WebFormsFluentValidations.Models
{
    public class RegisterVm
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }

    public class RegisterVmValidator : AbstractValidator<RegisterVm>
    {
        public RegisterVmValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is invalid.");

            RuleFor(x => x.Password)
                .NotEmpty().MinimumLength(8);

            RuleFor(x => x.RepeatPassword)
                .Equal(x => x.Password)
                .WithMessage("Passwords must match.");
        }
    }
}