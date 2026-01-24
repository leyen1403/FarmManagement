using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.SaleTypes.Queries.GetAllSaleTypes;

public record GetAllSaleTypesQuery() : IQuery<List<SaleTypeDto>>;
