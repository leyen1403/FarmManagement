// ***********************************************************************
// File: ICommandHandler.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa interface cơ sở cho Command Handler trong CQRS pattern.
// ***********************************************************************

using MediatR;

namespace FarmManagement.Application.Common.CQRS;

/// <summary>
/// Interface cho Command Handler không có kết quả trả về.
/// </summary>
/// <typeparam name="TCommand">Kiểu Command cần xử lý.</typeparam>
public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Unit>
    where TCommand : ICommand
{
}

/// <summary>
/// Interface cho Command Handler có kết quả trả về.
/// </summary>
/// <typeparam name="TCommand">Kiểu Command cần xử lý.</typeparam>
/// <typeparam name="TResult">Kiểu dữ liệu của kết quả trả về.</typeparam>
public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
 where TCommand : ICommand<TResult>
{
}
