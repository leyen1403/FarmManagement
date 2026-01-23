using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Features.CostTypes.Queries.GetCostTypeById;

public record GetCostTypeByIdQuery(int Id) : IQuery<CostTypeDto?>;
