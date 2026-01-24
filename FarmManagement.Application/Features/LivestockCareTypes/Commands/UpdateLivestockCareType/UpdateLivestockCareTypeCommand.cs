using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockCareTypes.Commands.UpdateLivestockCareType;

public record UpdateLivestockCareTypeCommand(int Id, UpdateLivestockCareTypeDto Dto) : ICommand;
