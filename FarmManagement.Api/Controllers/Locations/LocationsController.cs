using FarmManagement.Application.Features.Locations.Commands.CreateLocation;
using FarmManagement.Application.Features.Locations.Commands.UpdateLocation;
using FarmManagement.Application.Features.Locations.Commands.DeleteLocation;
using FarmManagement.Application.Features.Locations.Queries.GetAllLocations;
using FarmManagement.Application.Features.Locations.Queries.GetLocationById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Locations
{
    /// <summary>
    /// Controller quản lý Location.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Khởi tạo LocationsController với IMediator.
        /// </summary>
        /// <param name="mediator">MediatR mediator.</param>
        public LocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Lấy tất cả Location.
        /// </summary>
        /// <param name="activeOnly">Nếu true, chỉ lấy các Location đang hoạt động.</param>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool activeOnly = false)
        {
            var query = new GetAllLocationsQuery(activeOnly);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Lấy Location theo ID.
        /// </summary>
        /// <param name="id">ID của Location.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetLocationByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Tạo mới Location.
        /// </summary>
        /// <param name="command">Thông tin Location cần tạo.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLocationCommand command)
        {
            var location = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = location.Id }, location);
        }

        /// <summary>
        /// Cập nhật Location.
        /// </summary>
        /// <param name="id">ID của Location.</param>
        /// <param name="command">Thông tin cập nhật.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLocationCommand command)
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
        /// Xóa Location.
        /// </summary>
        /// <param name="id">ID của Location cần xóa.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteLocationCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}