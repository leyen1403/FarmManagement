using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockSales.Queries.GetLivestockSalesByLivestockId;

public record GetLivestockSalesByLivestockIdQuery(int LivestockId) : IQuery<List<LivestockSaleDto>>;
