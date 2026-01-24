using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockSales.Commands.UpdateLivestockSale;

public record UpdateLivestockSaleCommand(int Id, UpdateLivestockSaleDto Dto) : ICommand;
