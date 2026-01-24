using FarmManagement.Application.Common.CQRS;
using FarmManagement.Application.DTOs.Livestocks;

namespace FarmManagement.Application.Features.Livestocks.Commands.CreateLivestock;

public record CreateLivestockCommand(CreateLivestockDto Dto) : ICommand<int>;
