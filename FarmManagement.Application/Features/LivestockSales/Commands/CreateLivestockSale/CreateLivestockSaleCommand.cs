using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockSales.Commands.CreateLivestockSale;

public record CreateLivestockSaleCommand(CreateLivestockSaleDto Dto) : ICommand<int>;
