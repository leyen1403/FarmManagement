using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockTypes.Queries.GetAllLivestockTypes;

public record GetAllLivestockTypesQuery(bool IncludeInactive = false) : IQuery<List<LivestockTypeDto>>;
