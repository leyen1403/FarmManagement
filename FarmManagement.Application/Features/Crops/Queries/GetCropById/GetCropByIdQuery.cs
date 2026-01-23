using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Features.Crops.Queries.GetCropById;

/// <summary>
/// Query lấy cây trồng theo ID.
/// </summary>
public record GetCropByIdQuery(int Id) : IQuery<CropDto?>;
