using FluentValidation;
using WA.Application.Contracts.Request.RequestCategory;

namespace WA.Application.Validators.ValidatorsCategory
{
    public sealed class CategoryPutRequestValidator : AbstractValidator<CategoryPutRequest>
    {
        public CategoryPutRequestValidator()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull();
            RuleFor(c => c.Description).NotEmpty().NotNull();
        }
    }
}
