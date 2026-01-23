// ***********************************************************************
// File: GetCropTypeByIdQuery.cs
// Project: FarmManagement.Application
// Mô tả: Query để lấy loại cây trồng theo ID.
// ***********************************************************************

using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Crops;

namespace FarmManagement.Application.Features.CropTypes.Queries.GetCropTypeById;

/// <summary>
/// Query lấy loại cây trồng theo ID.
/// </summary>
/// <param name="Id">ID của loại cây trồng cần lấy.</param>
public record GetCropTypeByIdQuery(int Id) : IQuery<CropTypeDto?>;
