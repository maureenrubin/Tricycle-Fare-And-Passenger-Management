using MediatR;
using Microsoft.AspNetCore.Mvc;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Application.Features.Passengers.Commands;
using TricycleFareAndPassengerManagement.Application.Features.Passengers.Queries;

namespace TricycleFareAndPassengerManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PassengersController(IMediator _mediator) : ControllerBase
    {
        #region Public Methods

        [HttpPost("create")]
        public async Task<ActionResult<int>> CreatePassenger([FromBody] CreatePassengerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("getall")]
        public async Task<ActionResult<List<PassengerDto>>> GetAllPassengers()
        {
            var query = new GetAllPassengersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<bool>> UpdatePassenger(int id, [FromBody] UpdatePassengerCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in request body.");

            var result = await _mediator.Send(command);

            if (!result)
                return NotFound($"Passenger with ID {id} not found.");

            return Ok(result);
        }

        #endregion Public Methods
    }
}