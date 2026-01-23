// ***********************************************************************
// File: GetAllCropTypesQuery.cs
// Project: FarmManagement.Application
// Mô tả: Query để lấy tất cả loại cây trồng.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Features.CropTypes.Queries.GetAllCropTypes;

/// <summary>
/// Query lấy tất cả loại cây trồng.
/// </summary>
/// <param name="IncludeInactive">Nếu true, bao gồm cả loại cây trồng không hoạt động.</param>
public record GetAllCropTypesQuery(bool IncludeInactive = false) : IQuery<List<CropTypeDto>>;
