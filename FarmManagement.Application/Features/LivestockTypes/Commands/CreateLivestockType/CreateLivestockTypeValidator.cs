using FluentValidation;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockTypes.Commands.CreateLivestockType;

public class CreateLivestockTypeValidator : AbstractValidator<CreateLivestockTypeDto>
{
    public CreateLivestockTypeValidator()
    {
        RuleFor(x => x.Code)
.NotEmpty().WithMessage("Mã loại vật nuôi không được để trống.")
    .MaximumLength(20).WithMessage("Mã loại vật nuôi không được vượt quá 20 ký tự.");

        RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("Tên loại vật nuôi không được để trống.")
      .MaximumLength(100).WithMessage("Tên loại vật nuôi không được vượt quá 100 ký tự.");

        RuleFor(x => x.Description)
 .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.");
    }
}
