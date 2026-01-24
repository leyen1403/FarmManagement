using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockStatuses.Commands.CreateLivestockStatus;

public record CreateLivestockStatusCommand(CreateLivestockStatusDto Dto) : ICommand<int>;
