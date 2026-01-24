using FluentValidation;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.SaleTypes.Commands.CreateSaleType;

public class CreateSaleTypeValidator : AbstractValidator<CreateSaleTypeDto>
{
    public CreateSaleTypeValidator()
    {
        RuleFor(x => x.Code)
    .NotEmpty().WithMessage("Mã loại bán không được để trống.")
   .MaximumLength(20).WithMessage("Mã loại bán không được vượt quá 20 ký tự.");

        RuleFor(x => x.Name)
      .NotEmpty().WithMessage("Tên loại bán không được để trống.")
.MaximumLength(100).WithMessage("Tên loại bán không được vượt quá 100 ký tự.");
    }
}
