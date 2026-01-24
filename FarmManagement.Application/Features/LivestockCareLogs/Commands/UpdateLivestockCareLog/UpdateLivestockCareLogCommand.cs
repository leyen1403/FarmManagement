using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockCareLogs.Commands.UpdateLivestockCareLog;

public record UpdateLivestockCareLogCommand(int Id, UpdateLivestockCareLogDto Dto) : ICommand;
