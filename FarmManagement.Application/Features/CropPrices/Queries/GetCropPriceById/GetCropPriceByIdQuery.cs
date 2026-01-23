using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Features.CropPrices.Queries.GetCropPriceById;

public record GetCropPriceByIdQuery(int Id) : IQuery<CropPriceDto?>;
