using FluentValidation;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockHealthLogs.Commands.UpdateLivestockHealthLog;

public class UpdateLivestockHealthLogValidator : AbstractValidator<UpdateLivestockHealthLogDto>
{
    public UpdateLivestockHealthLogValidator()
    {
        RuleFor(x => x.Id)
 .GreaterThan(0).WithMessage("ID nhật ký sức khỏe không hợp lệ.");

        RuleFor(x => x.HealthStatusId)
   .GreaterThan(0).WithMessage("Trạng thái sức khỏe không hợp lệ.");

        RuleFor(x => x.CheckDate)
.NotEmpty().WithMessage("Ngày kiểm tra không được để trống.")
  .LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày kiểm tra không được lớn hơn ngày hiện tại.");

        RuleFor(x => x.Symptom)
     .MaximumLength(500).WithMessage("Triệu chứng không được vượt quá 500 ký tự.");

        RuleFor(x => x.Treatment)
            .MaximumLength(500).WithMessage("Phương pháp điều trị không được vượt quá 500 ký tự.");

        RuleFor(x => x.MedicineCost)
        .GreaterThanOrEqualTo(0).WithMessage("Chi phí thuốc men không được âm.");

        RuleFor(x => x.VetName)
  .MaximumLength(100).WithMessage("Tên bác sĩ thú y không được vượt quá 100 ký tự.");
    }
}
