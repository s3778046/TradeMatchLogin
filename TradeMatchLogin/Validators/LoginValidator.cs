using FluentValidation;
using TradeMatchLogin.Models;

namespace TradeMatchLogin.Validator
{
    public class LoginValidator : AbstractValidator<Login>
    {

        public LoginValidator()
        {
            RuleFor(u => u.UserName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(u => u.PasswordHash).NotNull().NotEmpty().MaximumLength(94);
        }
    }
}
