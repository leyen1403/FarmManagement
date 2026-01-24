using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockHealthStatuses.Commands.UpdateLivestockHealthStatus;

public record UpdateLivestockHealthStatusCommand(int Id, UpdateLivestockHealthStatusDto Dto) : ICommand;
