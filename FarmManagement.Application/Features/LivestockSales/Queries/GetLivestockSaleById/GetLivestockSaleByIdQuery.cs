using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockSales.Queries.GetLivestockSaleById;

public record GetLivestockSaleByIdQuery(int Id) : IQuery<LivestockSaleDto?>;
