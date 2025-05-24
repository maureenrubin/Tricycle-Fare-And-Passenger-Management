using MediatR;
using Microsoft.AspNetCore.Mvc;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Application.Features.Trips.Commands;
using TricycleFareAndPassengerManagement.Application.Features.Trips.Queries;

namespace TricycleFareAndPassengerManagement.Api.Controllers
{
    public class TripController(IMediator _mediator) : Controller
    {
        #region Public Methods

        [HttpPost("trips")]
        public async Task<ActionResult<int>> CreateTrip([FromBody] CreateTripCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("trips")]
        public async Task<ActionResult<List<TripDto>>> GetAllTrips()
        {
            var query = new GetAllTripsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        #endregion Public Methods
    }
}