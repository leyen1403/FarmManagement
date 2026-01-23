using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Features.CostTypes.Queries.GetAllCostTypes;

public record GetAllCostTypesQuery(bool IncludeInactive = false) : IQuery<IEnumerable<CostTypeDto>>;
