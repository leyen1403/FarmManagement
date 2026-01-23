// ***********************************************************************
// File: GetAllCropStatusesQuery.cs
// Project: FarmManagement.Application
// Mô tả: Query để lấy tất cả trạng thái cây trồng.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Features.CropStatuses.Queries.GetAllCropStatuses;

/// <summary>
/// Query lấy tất cả trạng thái cây trồng.
/// </summary>
/// <param name="IncludeInactive">Nếu true, bao gồm cả trạng thái không hoạt động.</param>
public record GetAllCropStatusesQuery(bool IncludeInactive = false) : IQuery<IEnumerable<CropStatusDto>>;
