using FluentValidation;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockHealthStatuses.Commands.UpdateLivestockHealthStatus;

public class UpdateLivestockHealthStatusValidator : AbstractValidator<UpdateLivestockHealthStatusDto>
{
    public UpdateLivestockHealthStatusValidator()
    {
        RuleFor(x => x.Id)
     .GreaterThan(0).WithMessage("ID trạng thái sức khỏe không hợp lệ.");

        RuleFor(x => x.Code)
        .NotEmpty().WithMessage("Mã trạng thái sức khỏe không được để trống.")
           .MaximumLength(20).WithMessage("Mã trạng thái sức khỏe không được vượt quá 20 ký tự.");

        RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Tên trạng thái sức khỏe không được để trống.")
      .MaximumLength(100).WithMessage("Tên trạng thái sức khỏe không được vượt quá 100 ký tự.");
    }
}
