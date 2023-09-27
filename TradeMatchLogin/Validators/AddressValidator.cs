using FluentValidation;
using TradeMatchLogin.Models;

namespace TradeMatchLogin.Validator
{
    public class AddressValidator : AbstractValidator<Address>
    {

        public AddressValidator()
        {
            RuleFor(u => u.Number).NotNull().NotEmpty();
            RuleFor(u => u.Street).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(u => u.Suburb).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(u => u.PostCode).NotNull().NotEmpty();
            RuleFor(u => u.State).NotNull().NotEmpty().MaximumLength(4); ;
        }
    }
}
