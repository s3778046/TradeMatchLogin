using FluentValidation;
using TradeMatchLogin.Models;

namespace TradeMatchLogin.Validator
{
    public class UserValidator : AbstractValidator<User>
    {

        public UserValidator()
        {
            RuleFor(u => u.FirstName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(u => u.LastName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(u => u.Phone).NotNull().NotEmpty().MaximumLength(15);
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.ABN).MaximumLength(50);
            RuleFor(u => u.BusinessName).MaximumLength(50);
            RuleFor(u => u.Status).NotNull().NotEmpty().MaximumLength(15);
            RuleFor(u => u.Role).NotNull().NotEmpty().MaximumLength(15);
        }
    }
}
