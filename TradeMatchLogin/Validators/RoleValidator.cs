using FluentValidation;
using TradeMatchLogin.Models;

namespace TradeMatchLogin.Validator
{
    public class RoleValidator : AbstractValidator<Role>
    {

        public RoleValidator()
        {
            RuleFor(u => u.RoleType).NotNull().NotEmpty().MaximumLength(20);
        }
    }
}
