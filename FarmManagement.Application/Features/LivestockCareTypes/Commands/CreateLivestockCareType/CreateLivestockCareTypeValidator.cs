using FluentValidation;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockCareTypes.Commands.CreateLivestockCareType;

public class CreateLivestockCareTypeValidator : AbstractValidator<CreateLivestockCareTypeDto>
{
    public CreateLivestockCareTypeValidator()
    {
        RuleFor(x => x.Code)
  .NotEmpty().WithMessage("Mã loại chăm sóc không được để trống.")
       .MaximumLength(20).WithMessage("Mã loại chăm sóc không được vượt quá 20 ký tự.");

        RuleFor(x => x.Name)
  .NotEmpty().WithMessage("Tên loại chăm sóc không được để trống.")
 .MaximumLength(100).WithMessage("Tên loại chăm sóc không được vượt quá 100 ký tự.");

        RuleFor(x => x.Description)
      .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.");
    }
}
