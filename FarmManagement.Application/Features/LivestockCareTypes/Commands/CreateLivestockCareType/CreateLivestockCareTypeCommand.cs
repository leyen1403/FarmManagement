using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockCareTypes.Commands.CreateLivestockCareType;

public record CreateLivestockCareTypeCommand(CreateLivestockCareTypeDto Dto) : ICommand<int>;
