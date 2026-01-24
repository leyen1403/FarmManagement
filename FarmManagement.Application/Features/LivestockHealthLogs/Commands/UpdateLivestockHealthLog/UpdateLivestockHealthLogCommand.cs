using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockHealthLogs.Commands.UpdateLivestockHealthLog;

public record UpdateLivestockHealthLogCommand(int Id, UpdateLivestockHealthLogDto Dto) : ICommand;
