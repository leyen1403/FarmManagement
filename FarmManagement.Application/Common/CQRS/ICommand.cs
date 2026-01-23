// ***********************************************************************
// File: ICommand.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa interface cơ sở cho Command trong CQRS pattern.
// ***********************************************************************

using MediatR;

namespace FarmManagement.Application.Common.CQRS;

/// <summary>
/// Interface đánh dấu một Command không có kết quả trả về.
/// </summary>
public interface ICommand : IRequest<Unit>
{
}

/// <summary>
/// Interface đánh dấu một Command có kết quả trả về.
/// </summary>
/// <typeparam name="TResult">Kiểu dữ liệu của kết quả trả về.</typeparam>
public interface ICommand<out TResult> : IRequest<TResult>
{
}
