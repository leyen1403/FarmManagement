using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Features.CropCareLogs.Queries.GetCropCareLogById;

public record GetCropCareLogByIdQuery(int Id) : IQuery<CropCareLogDto?>;
