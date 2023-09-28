using FluentValidation;
using TradeMatchLogin.Dtos;
using TradeMatchLogin.Models;

namespace TradeMatchLogin.Validators.DtoValidators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {

        public LoginDtoValidator()
        {
            RuleFor(u => u.UserName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(u => u.Password).NotNull().NotEmpty().MaximumLength(94);
        }
    }
}
