using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockTypes.Commands.UpdateLivestockType;

public record UpdateLivestockTypeCommand(int Id, UpdateLivestockTypeDto Dto) : ICommand;
