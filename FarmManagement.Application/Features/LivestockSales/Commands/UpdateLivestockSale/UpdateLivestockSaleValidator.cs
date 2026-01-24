using FluentValidation;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockSales.Commands.UpdateLivestockSale;

public class UpdateLivestockSaleValidator : AbstractValidator<UpdateLivestockSaleDto>
{
    public UpdateLivestockSaleValidator()
    {
        RuleFor(x => x.Id)
  .GreaterThan(0).WithMessage("ID đơn hàng không hợp lệ.");

        RuleFor(x => x.SaleTypeId)
       .GreaterThan(0).WithMessage("Loại bán không hợp lệ.");

        RuleFor(x => x.SaleDate)
      .NotEmpty().WithMessage("Ngày bán không được để trống.")
     .LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày bán không được lớn hơn ngày hiện tại.");

        RuleFor(x => x.Details)
   .NotEmpty().WithMessage("Đơn hàng phải có ít nhất một chi tiết.");

        RuleFor(x => x.Buyer)
    .MaximumLength(200).WithMessage("Tên người mua không được vượt quá 200 ký tự.");

        RuleFor(x => x.BuyerPhone)
             .MaximumLength(20).WithMessage("Số điện thoại không được vượt quá 20 ký tự.");

        RuleFor(x => x.Note)
            .MaximumLength(1000).WithMessage("Ghi chú không được vượt quá 1000 ký tự.");

        // Validate each detail
        RuleForEach(x => x.Details).ChildRules(detail =>
{
    detail.RuleFor(d => d.Weight)
.GreaterThan(0).WithMessage("Trọng lượng phải lớn hơn 0.");

    detail.RuleFor(d => d.UnitPrice)
  .GreaterThan(0).WithMessage("Đơn giá phải lớn hơn 0.");
});
    }
}
