using MediatR;
using TricycleFareAndPassengerManagement.Domain.Interfaces;

namespace TricycleFareAndPassengerManagement.Application.Features.Passengers.Commands
{
    public class UpdatePassengerCommandHandler : IRequestHandler<UpdatePassengerCommand, bool>
    {
        #region Fields

        private readonly IAppDbContext _context;

        #endregion Fields

        #region Public Constructors

        public UpdatePassengerCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<bool> Handle(UpdatePassengerCommand request, CancellationToken cancellationToken)
        {
            var passenger = await _context.Passengers.FindAsync(request.Id);
            if (passenger == null)
                return false;

            passenger.FullName = request.FullName;
            passenger.PhoneNumber = request.PhoneNumber;
            passenger.IsActive = request.IsActive;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        #endregion Public Methods
    }
}