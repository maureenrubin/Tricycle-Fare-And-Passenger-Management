using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Application.Features.Fares.Queries;

namespace TricycleFareAndPassengerManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FareController(IMediator _mediator) : ControllerBase
    {
        #region Public Methods

        [HttpPost("calculate")]
        public async Task<ActionResult<FareCalculationDto>> CalculateFare([FromBody] CalculateFareQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        #endregion Public Methods
    }
}