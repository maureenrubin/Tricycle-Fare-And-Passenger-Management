using MediatR;
using Microsoft.AspNetCore.Mvc;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Application.Features.Fares.Queries;

namespace TricycleFareAndPassengerManagement.Api.Controllers
{
    public class FareController(IMediator _mediator) : Controller
    {
        #region Public Methods

        [HttpPost("fares/calculate")]
        public async Task<ActionResult<FareCalculationDto>> CalculateFare([FromBody] CalculateFareQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        #endregion Public Methods
    }
}