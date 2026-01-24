using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockCareTypes.Queries.GetAllLivestockCareTypes;

public record GetAllLivestockCareTypesQuery() : IQuery<List<LivestockCareTypeDto>>;
