using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.Livestocks.Queries.GetAllLivestocks;

public record GetAllLivestocksQuery(int? LivestockTypeId = null, int? LivestockStatusId = null, int? LocationId = null) : IQuery<IEnumerable<LivestockDto>>;
