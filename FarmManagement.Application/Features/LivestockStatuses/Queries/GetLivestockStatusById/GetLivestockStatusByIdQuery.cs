using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockStatuses.Queries.GetLivestockStatusById;

public record GetLivestockStatusByIdQuery(int Id) : IQuery<LivestockStatusDto?>;
