using FluentValidation;
using TradeMatchLogin.DTOs;
using TradeMatchLogin.Models;

namespace TradeMatchLogin.Validators.DTOValidators
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {

        public LoginDTOValidator()
        {
            RuleFor(u => u.UserName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(u => u.Password).NotNull().NotEmpty().MaximumLength(50);

        }
    }
}
