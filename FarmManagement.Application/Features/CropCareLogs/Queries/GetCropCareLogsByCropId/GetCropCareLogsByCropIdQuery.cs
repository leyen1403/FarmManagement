using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Features.CropCareLogs.Queries.GetCropCareLogsByCropId;

public record GetCropCareLogsByCropIdQuery(int CropId) : IQuery<IEnumerable<CropCareLogDto>>;
