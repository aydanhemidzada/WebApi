using FluentValidation;
using WebApiConfigurationn.Entities.DTOs.Categories;

namespace WebApiConfigurationn.Validators.Categories
{
    public class CreateCategoryDTOValidators:AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryDTOValidators()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Ad bos ola bilmez")
                .MinimumLength(5).WithMessage("Minimum 5 simvoldan ibaret olmalidi ")
                .MaximumLength(30).WithMessage("Maximum 30  simvoldan ibaret olmalidi");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description bos ola bilmez")
                .NotNull().WithMessage("Description deyeri daxil olmalidi")
                .MinimumLength(10).WithMessage("Minimum 10 simvoldan ibaret olmalidi")
                .MaximumLength(40).WithMessage("Maximum 40  simvoldan ibaret olmalidi");

        }

    }
}
