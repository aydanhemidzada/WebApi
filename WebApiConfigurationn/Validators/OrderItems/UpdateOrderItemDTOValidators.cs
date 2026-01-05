using FluentValidation;
using WebApiConfigurationn.Entities.DTOs.OrderItems;

namespace WebApiConfigurationn.Validators.OrderItems
{
    public class UpdateOrderItemDTOValidators:AbstractValidator<UpdateOrderItemDTO>
    {
        public UpdateOrderItemDTOValidators()
        {

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Bos ola bilmez")
                .NotNull().WithMessage("Bos ola bilmez")
                .MaximumLength(500).WithMessage("Maksimum 500 simvol ola biler");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Miqdar boş ola bilmez")
                .NotNull().WithMessage("Miqdar null ola bilmez");

            RuleFor(x => x.UnitPrice)
                .NotEmpty().WithMessage("Bos ola bilmez")
                .NotNull().WithMessage("Bos ola bilməe");

        }
    }
}
