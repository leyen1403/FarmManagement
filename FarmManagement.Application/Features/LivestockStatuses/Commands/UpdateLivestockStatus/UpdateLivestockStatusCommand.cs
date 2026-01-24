using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockStatuses.Commands.UpdateLivestockStatus;

public record UpdateLivestockStatusCommand(int Id, UpdateLivestockStatusDto Dto) : ICommand;
