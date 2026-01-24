using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockHealthStatuses.Commands.CreateLivestockHealthStatus;

public record CreateLivestockHealthStatusCommand(CreateLivestockHealthStatusDto Dto) : ICommand<int>;
