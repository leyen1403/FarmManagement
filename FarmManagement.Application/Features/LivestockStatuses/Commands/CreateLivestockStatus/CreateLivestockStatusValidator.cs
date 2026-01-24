using FluentValidation;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockStatuses.Commands.CreateLivestockStatus;

public class CreateLivestockStatusValidator : AbstractValidator<CreateLivestockStatusDto>
{
    public CreateLivestockStatusValidator()
    {
        RuleFor(x => x.Code)
  .NotEmpty().WithMessage("Mã trạng thái không được để trống.")
   .MaximumLength(20).WithMessage("Mã trạng thái không được vượt quá 20 ký tự.");

        RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("Tên trạng thái không được để trống.")
        .MaximumLength(100).WithMessage("Tên trạng thái không được vượt quá 100 ký tự.");

        RuleFor(x => x.Description)
   .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.");
    }
}
