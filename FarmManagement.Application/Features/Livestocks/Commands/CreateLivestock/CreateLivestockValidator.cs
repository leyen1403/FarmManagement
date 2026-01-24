using FluentValidation;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.Livestocks.Commands.CreateLivestock;

public class CreateLivestockValidator : AbstractValidator<CreateLivestockDto>
{
    public CreateLivestockValidator()
    {
        RuleFor(x => x.LivestockTypeId)
       .GreaterThan(0).WithMessage("Loại vật nuôi không hợp lệ.");

        RuleFor(x => x.LocationId)
   .GreaterThan(0).WithMessage("Địa điểm nuôi không hợp lệ.");

        RuleFor(x => x.LivestockStatusId)
 .GreaterThan(0).WithMessage("Trạng thái vật nuôi không hợp lệ.");

        RuleFor(x => x.ImportDate)
       .NotEmpty().WithMessage("Ngày nhập không được để trống.")
                  .LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày nhập không được lớn hơn ngày hiện tại.");

        RuleFor(x => x.ImportWeight)
 .GreaterThan(0).WithMessage("Trọng lượng nhập phải lớn hơn 0.");

        RuleFor(x => x.ImportPrice)
.GreaterThanOrEqualTo(0).WithMessage("Giá nhập không được âm.");

        RuleFor(x => x.TagCode)
      .MaximumLength(50).WithMessage("Mã thẻ không được vượt quá 50 ký tự.");

        RuleFor(x => x.Note)
 .MaximumLength(500).WithMessage("Ghi chú không được vượt quá 500 ký tự.");
    }
}
