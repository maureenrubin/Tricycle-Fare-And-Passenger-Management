using MediatR;
using Microsoft.AspNetCore.Mvc;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Application.Features.Reports.Queries;

namespace TricycleFareAndPassengerManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController(IMediator _mediator) : ControllerBase
    {
        #region Public Methods

        [HttpGet("getreports")]
        public async Task<ActionResult<DailyReportDto>> GetDailyReport([FromQuery] DateTime date)
        {
            var query = new GetDailyReportQuery { Date = date };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("getreportbydate/{date}")]
        public async Task<ActionResult<DailyReportDto>> GetDailyReportByDate(DateTime date)
        {
            var query = new GetDailyReportQuery { Date = date };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        #endregion Public Methods
    }
}