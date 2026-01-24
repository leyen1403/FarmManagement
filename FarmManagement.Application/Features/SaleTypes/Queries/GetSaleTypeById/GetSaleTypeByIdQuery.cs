using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.SaleTypes.Queries.GetSaleTypeById;

public record GetSaleTypeByIdQuery(int Id) : IQuery<SaleTypeDto?>;
