using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockHealthLogs.Queries.GetLivestockHealthLogsByLivestockId;

public record GetLivestockHealthLogsByLivestockIdQuery(int LivestockId) : IQuery<List<LivestockHealthLogDto>>;
