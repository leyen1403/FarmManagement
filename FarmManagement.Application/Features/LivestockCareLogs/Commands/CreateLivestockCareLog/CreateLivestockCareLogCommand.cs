using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockCareLogs.Commands.CreateLivestockCareLog;

public record CreateLivestockCareLogCommand(CreateLivestockCareLogDto Dto) : ICommand<int>;
