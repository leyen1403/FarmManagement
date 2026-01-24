using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.SaleTypes.Commands.UpdateSaleType;

public record UpdateSaleTypeCommand(int Id, UpdateSaleTypeDto Dto) : ICommand;
