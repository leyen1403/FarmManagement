using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockHealthLogs.Commands.CreateLivestockHealthLog;

public record CreateLivestockHealthLogCommand(CreateLivestockHealthLogDto Dto) : ICommand<int>;
