using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.Livestocks.Queries.GetLivestockById;

public record GetLivestockByIdQuery(int Id) : IQuery<LivestockDto?>;
