using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockStatuses.Queries.GetAllLivestockStatuses;

public record GetAllLivestockStatusesQuery(bool IncludeInactive = false) : IQuery<List<LivestockStatusDto>>;
