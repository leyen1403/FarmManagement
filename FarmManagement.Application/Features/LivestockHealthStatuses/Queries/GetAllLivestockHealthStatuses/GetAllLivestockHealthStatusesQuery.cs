using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockHealthStatuses.Queries.GetAllLivestockHealthStatuses;

public record GetAllLivestockHealthStatusesQuery() : IQuery<List<LivestockHealthStatusDto>>;
