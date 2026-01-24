using FluentValidation;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.SaleTypes.Commands.UpdateSaleType;

public class UpdateSaleTypeValidator : AbstractValidator<UpdateSaleTypeDto>
{
    public UpdateSaleTypeValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("ID loại bán không hợp lệ.");

        RuleFor(x => x.Code)
       .NotEmpty().WithMessage("Mã loại bán không được để trống.")
       .MaximumLength(20).WithMessage("Mã loại bán không được vượt quá 20 ký tự.");

        RuleFor(x => x.Name)
.NotEmpty().WithMessage("Tên loại bán không được để trống.")
   .MaximumLength(100).WithMessage("Tên loại bán không được vượt quá 100 ký tự.");
    }
}
