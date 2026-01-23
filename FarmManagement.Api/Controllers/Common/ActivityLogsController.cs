using FarmManagement.Application.Features.ActivityLogs.Queries.GetRecentActivities;
using FarmManagement.Application.Features.ActivityLogs.Queries.GetActivityCountToday;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Api.Controllers.Common;

/// <summary>
/// Controller quản lý Activity Log.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ActivityLogsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActivityLogsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy danh sách hoạt động gần đây.
    /// </summary>
    /// <param name="count">Số lượng hoạt động cần lấy (mặc định 10).</param>
    [HttpGet("recent")]
    public async Task<IActionResult> GetRecentActivities([FromQuery] int count = 10)
    {
        var query = new GetRecentActivitiesQuery(count);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Lấy số lượng hoạt động trong ngày hôm nay.
    /// </summary>
    [HttpGet("count-today")]
    public async Task<IActionResult> GetActivityCountToday()
    {
        var query = new GetActivityCountTodayQuery();
        var count = await _mediator.Send(query);
        return Ok(new { count });
    }
}
