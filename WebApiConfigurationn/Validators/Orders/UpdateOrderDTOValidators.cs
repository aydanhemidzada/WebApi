using FluentValidation;
using WebApiConfigurationn.Entities.DTOs.Orders;

namespace WebApiConfigurationn.Validators.Orders
{
    public class UpdateOrderDTOValidators:AbstractValidator<UpdateOrderDTO>
    {
        public UpdateOrderDTOValidators()
        {
            RuleFor(x => x.OrderDate)
               .NotEmpty().WithMessage("Sifaris tarixi bos ola bilmez")
               .LessThanOrEqualTo(DateTime.UtcNow)
               .WithMessage("Sifaris tarixi indiki vaxtdan boyuk ola bilmez");

            RuleFor(x => x.TotalAmount)
                .NotEmpty().WithMessage("Umumi mebleg bos ola bilmez")
                .NotNull().WithMessage("Umumi mebleg null ola bilmez");
        }
    }
}
