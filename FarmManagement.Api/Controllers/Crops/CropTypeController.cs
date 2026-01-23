using FarmManagement.Application.Features.CropTypes.Commands.CreateCropType;
using FarmManagement.Application.Features.CropTypes.Commands.DeleteCropType;
using FarmManagement.Application.Features.CropTypes.Commands.UpdateCropType;
using FarmManagement.Application.Features.CropTypes.Queries.GetAllCropTypes;
using FarmManagement.Application.Features.CropTypes.Queries.GetCropTypeById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Crops
{
    /// <summary>
    /// Controller quản lý loại cây trồng (CropType).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CropTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Khởi tạo CropTypeController với IMediator.
        /// </summary>
        /// <param name="mediator">MediatR mediator.</param>
        public CropTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Lấy tất cả loại cây trồng.
        /// </summary>
        /// <param name="includeInactive">Bao gồm loại không hoạt động.</param>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
        {
            var query = new GetAllCropTypesQuery(includeInactive);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Lấy loại cây trồng theo ID.
        /// </summary>
        /// <param name="id">ID của loại cây trồng.</param>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetCropTypeByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound(new { message = "CropType not found" });

            return Ok(result);
        }

        /// <summary>
        /// Tạo mới loại cây trồng.
        /// </summary>
        /// <param name="command">Thông tin loại cây trồng cần tạo.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCropTypeCommand command)
        {
            var newId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = newId }, null);
        }

        /// <summary>
        /// Cập nhật loại cây trồng.
        /// </summary>
        /// <param name="id">ID của loại cây trồng.</param>
        /// <param name="command">Thông tin cập nhật.</param>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCropTypeCommand command)
        {
            // Ensure the ID in the route matches the command
            if (id != command.Id)
            {
                command = command with { Id = id };
            }

            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Xóa loại cây trồng.
        /// </summary>
        /// <param name="id">ID của loại cây trồng cần xóa.</param>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteCropTypeCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
