using FluentValidation;
using WA.Application.Contracts.Request.RequestCategory;

namespace WA.Application.Validators.ValidatorsCategory
{
    public sealed class CategoryPostRequestValidator : AbstractValidator<CategoryPostRequest>
    {
        public CategoryPostRequestValidator()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull();
            RuleFor(c => c.Description).NotEmpty().NotNull();
        }
    }
}
