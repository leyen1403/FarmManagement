// ***********************************************************************
// File: IQuery.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa interface cơ sở cho Query trong CQRS pattern.
// ***********************************************************************

using MediatR;

namespace FarmManagement.Application.Common.CQRS;

/// <summary>
/// Interface đánh dấu một Query có kết quả trả về.
/// </summary>
/// <typeparam name="TResult">Kiểu dữ liệu của kết quả trả về.</typeparam>
public interface IQuery<out TResult> : IRequest<TResult>
{
}
