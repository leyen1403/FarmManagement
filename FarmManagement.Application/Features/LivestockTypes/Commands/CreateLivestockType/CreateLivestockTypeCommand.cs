using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.LivestockTypes.Commands.CreateLivestockType;

public record CreateLivestockTypeCommand(CreateLivestockTypeDto Dto) : ICommand<int>;
