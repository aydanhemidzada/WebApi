using FluentValidation;
using WebApiConfigurationn.Entities.DTOs.Products;

namespace WebApiConfigurationn.Validators.Products
{
    public class UpdateProductDTOValidators:AbstractValidator<UpdateProductDTO>
    {
        public UpdateProductDTOValidators()
        {
            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("Ad bos olmamalidi")
               .NotNull().WithMessage("Ad bos olmamalidi")
               .MinimumLength(5).WithMessage("Mininum 5 simvoldan daxil edilmeldi")
               .MaximumLength(30).WithMessage("Maximum 30 simvoldan daxil edilmeldi");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description bos ola bilmez")
                .NotNull().WithMessage("Description bos ola bilmez")
                .MinimumLength(10).WithMessage("Minimum 10 simvoldan ibaret olmalidi")
                .MaximumLength(40).WithMessage("Maximum 40  simvoldan ibaret olmalidi");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Qiymet bos ola bilmez")
                .NotNull().WithMessage("Qiymet bos ola bilmez");


            RuleFor(p => p.Count)
                .NotEmpty().WithMessage("Say bos ola bilmez")
                .NotNull().WithMessage("Say bos ola bilmez");
                

            RuleFor(p => p.Currency)
                .NotEmpty().WithMessage("Valyuta boş ola bilmez")
                .NotNull().WithMessage("Valyuta boş ola bilmez")
                .Length(3).WithMessage("Valyuta 3 simvoldan ibarət olmalıdır");
              

        }
    }
}
