using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.Livestocks.Commands.UpdateLivestock;

public record UpdateLivestockCommand(int Id, UpdateLivestockDto Dto) : ICommand;
