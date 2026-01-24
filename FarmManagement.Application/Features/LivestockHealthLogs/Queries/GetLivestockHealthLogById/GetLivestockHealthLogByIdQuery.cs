using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockHealthLogs.Queries.GetLivestockHealthLogById;

public record GetLivestockHealthLogByIdQuery(int Id) : IQuery<LivestockHealthLogDto?>;
