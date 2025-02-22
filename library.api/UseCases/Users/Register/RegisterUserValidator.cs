using FluentValidation;
using library.communication.Requests;

namespace library.api.UseCases.Users.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(user => user.Email).EmailAddress().WithMessage("Invalid email");
            RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required");
            When(user => !string.IsNullOrEmpty(user.Password), () => { RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage("Password length should be greater than 6"); });

        }
    }
}
