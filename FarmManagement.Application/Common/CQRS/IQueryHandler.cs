// ***********************************************************************
// File: IQueryHandler.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa interface cơ sở cho Query Handler trong CQRS pattern.
// ***********************************************************************

using MediatR;

namespace FarmManagement.Application.Common.CQRS;

/// <summary>
/// Interface cho Query Handler có kết quả trả về.
/// </summary>
/// <typeparam name="TQuery">Kiểu Query cần xử lý.</typeparam>
/// <typeparam name="TResult">Kiểu dữ liệu của kết quả trả về.</typeparam>
public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
}
