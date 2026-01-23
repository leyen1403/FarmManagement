// ***********************************************************************
// File: GetCropStatusByIdQuery.cs
// Project: FarmManagement.Application
// Mô tả: Query để lấy trạng thái cây trồng theo ID.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Features.CropStatuses.Queries.GetCropStatusById;

/// <summary>
/// Query lấy trạng thái cây trồng theo ID.
/// </summary>
/// <param name="Id">ID của trạng thái cây trồng cần lấy.</param>
public record GetCropStatusByIdQuery(int Id) : IQuery<CropStatusDto>;
