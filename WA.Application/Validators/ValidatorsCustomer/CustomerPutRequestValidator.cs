using FluentValidation;
using WA.Application.Contracts.Request.RequestCustomer;

namespace WA.Application.Validators.ValidatorsCustomer
{
    public sealed class CustomerPutRequestValidator : AbstractValidator<CustomerPutRequest>
    {
        public CustomerPutRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
        }
    }
}
