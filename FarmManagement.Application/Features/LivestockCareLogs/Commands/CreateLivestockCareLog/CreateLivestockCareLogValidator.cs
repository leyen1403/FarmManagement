using FluentValidation;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockCareLogs.Commands.CreateLivestockCareLog;

public class CreateLivestockCareLogValidator : AbstractValidator<CreateLivestockCareLogDto>
{
    public CreateLivestockCareLogValidator()
    {
        RuleFor(x => x.LivestockId)
.GreaterThan(0).WithMessage("Vật nuôi không hợp lệ.");

        RuleFor(x => x.LivestockCareTypeId)
      .GreaterThan(0).WithMessage("Loại chăm sóc không hợp lệ.");

        RuleFor(x => x.CareDate)
    .NotEmpty().WithMessage("Ngày chăm sóc không được để trống.")
   .LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày chăm sóc không được lớn hơn ngày hiện tại.");

        RuleFor(x => x.Quantity)
        .GreaterThanOrEqualTo(0).WithMessage("Số lượng không được âm.");

        RuleFor(x => x.Unit)
    .MaximumLength(20).WithMessage("Đơn vị không được vượt quá 20 ký tự.");

        RuleFor(x => x.Cost)
   .GreaterThanOrEqualTo(0).WithMessage("Chi phí không được âm.");

        RuleFor(x => x.Note)
 .MaximumLength(500).WithMessage("Ghi chú không được vượt quá 500 ký tự.");
    }
}
