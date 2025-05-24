using MediatR;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;

namespace TricycleFareAndPassengerManagement.Application.Features.Driver.Queries
{
    public class GetDriverByIdQuery : IRequest<DriverDto?>
    {
        #region Properties

        public int Id { get; set; }

        #endregion Properties
    }
}