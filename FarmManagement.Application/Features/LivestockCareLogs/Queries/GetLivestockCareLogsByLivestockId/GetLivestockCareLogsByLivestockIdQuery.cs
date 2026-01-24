using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockCareLogs.Queries.GetLivestockCareLogsByLivestockId;

public record GetLivestockCareLogsByLivestockIdQuery(int LivestockId) : IQuery<List<LivestockCareLogDto>>;
