using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.SaleTypes.Commands.CreateSaleType;

public record CreateSaleTypeCommand(CreateSaleTypeDto Dto) : ICommand<int>;
