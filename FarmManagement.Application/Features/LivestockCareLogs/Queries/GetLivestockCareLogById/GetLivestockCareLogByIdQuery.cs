using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockCareLogs.Queries.GetLivestockCareLogById;

public record GetLivestockCareLogByIdQuery(int Id) : IQuery<LivestockCareLogDto?>;
