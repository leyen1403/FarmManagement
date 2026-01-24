using FluentValidation;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockTypes.Commands.UpdateLivestockType;

public class UpdateLivestockTypeValidator : AbstractValidator<UpdateLivestockTypeDto>
{
    public UpdateLivestockTypeValidator()
    {
        RuleFor(x => x.Id)
     .GreaterThan(0).WithMessage("ID loại vật nuôi không hợp lệ.");

        RuleFor(x => x.Name)
       .NotEmpty().WithMessage("Tên loại vật nuôi không được để trống.")
        .MaximumLength(100).WithMessage("Tên loại vật nuôi không được vượt quá 100 ký tự.");

        RuleFor(x => x.Description)
       .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.");
    }
}
