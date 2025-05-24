using MediatR;
using TricycleFareAndPassengerManagement.Domain.Entities;
using TricycleFareAndPassengerManagement.Domain.Interfaces;

namespace TricycleFareAndPassengerManagement.Application.Features.Passengers.Commands
{
    public class CreatePassengerCommandHandler : IRequestHandler<CreatePassengerCommand, int>
    {
        #region Fields

        private readonly IAppDbContext _context;

        #endregion Fields

        #region Public Constructors

        public CreatePassengerCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> Handle(CreatePassengerCommand request, CancellationToken cancellationToken)
        {
            var passenger = new Passenger
            {
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber
            };

            _context.Passengers.Add(passenger);
            await _context.SaveChangesAsync(cancellationToken);

            return passenger.Id;
        }

        #endregion Public Methods
    }
}