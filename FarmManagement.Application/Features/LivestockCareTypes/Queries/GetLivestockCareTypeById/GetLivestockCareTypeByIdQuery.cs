using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockCareTypes.Queries.GetLivestockCareTypeById;

public record GetLivestockCareTypeByIdQuery(int Id) : IQuery<LivestockCareTypeDto?>;
