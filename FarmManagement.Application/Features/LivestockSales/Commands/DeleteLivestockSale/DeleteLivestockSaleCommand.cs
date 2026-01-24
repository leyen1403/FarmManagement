using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.LivestockSales.Commands.DeleteLivestockSale;

public record DeleteLivestockSaleCommand(int Id) : ICommand;
