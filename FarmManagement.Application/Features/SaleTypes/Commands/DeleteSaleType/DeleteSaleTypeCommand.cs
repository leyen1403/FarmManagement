using FarmManagement.Application.Common.CQRS;

namespace FarmManagement.Application.Features.SaleTypes.Commands.DeleteSaleType;

public record DeleteSaleTypeCommand(int Id) : ICommand;
