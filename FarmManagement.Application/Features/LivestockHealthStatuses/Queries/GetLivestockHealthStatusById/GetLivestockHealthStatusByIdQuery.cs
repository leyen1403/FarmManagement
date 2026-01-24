using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockHealthStatuses.Queries.GetLivestockHealthStatusById;

public record GetLivestockHealthStatusByIdQuery(int Id) : IQuery<LivestockHealthStatusDto?>;
