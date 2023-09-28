using FluentValidation;
using TradeMatchLogin.Dtos;

namespace TradeMatchLogin.Validators.DtoValidators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {

        public RegisterDtoValidator()
        {

            // Login
            RuleFor(u => u.UserName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(u => u.Password).NotNull().NotEmpty().MaximumLength(50);

            // Address
            RuleFor(u => u.Number).NotNull().NotEmpty();
            RuleFor(u => u.Street).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(u => u.Suburb).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(u => u.PostCode).NotNull().NotEmpty();
            RuleFor(u => u.State).NotNull().NotEmpty().MaximumLength(4);

            // User
            RuleFor(u => u.FirstName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(u => u.LastName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(u => u.Phone).NotNull().NotEmpty().MaximumLength(15);
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.ABN).MaximumLength(50);
            RuleFor(u => u.BusinessName).MaximumLength(50);

        }
    }
}
