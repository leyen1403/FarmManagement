using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Features.CropPrices.Queries.GetCropPriceHistory;

/// <summary>
/// Query lấy lịch sử giá của một cây trồng.
/// </summary>
public record GetCropPriceHistoryQuery(int CropId) : IQuery<IEnumerable<CropPriceDto>>;
