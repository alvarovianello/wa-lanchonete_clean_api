using FluentValidation;
using WA.Application.Contracts.Request.RequestCustomer;

namespace WA.Application.Validators.ValidatorsCustomer
{
    public sealed class CustomerPostRequestValidator : AbstractValidator<CustomerPostRequest>
    {
        public CustomerPostRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Cpf).NotEmpty().NotNull();
        }
    }
}
