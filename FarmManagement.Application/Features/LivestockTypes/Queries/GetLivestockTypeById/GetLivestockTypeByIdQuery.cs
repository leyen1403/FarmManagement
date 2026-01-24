using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockTypes.Queries.GetLivestockTypeById;

public record GetLivestockTypeByIdQuery(int Id) : IQuery<LivestockTypeDto?>;
