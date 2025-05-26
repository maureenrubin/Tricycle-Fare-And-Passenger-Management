using MediatR;
using TricycleFareAndPassengerManagement.Domain.Interfaces;

namespace TricycleFareAndPassengerManagement.Application.Features.Driver.Commands
{
    public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, bool>
    {
        #region Fields

        private readonly IAppDbContext _context;

        #endregion Fields

        #region Public Constructors

        public UpdateDriverCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<bool> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = await _context.Drivers.FindAsync(request.Id);
            if (driver == null)
                return false;

            driver.FullName = request.FullName;
            driver.LicenseNumber = request.LicenseNumber;
            driver.PhoneNumber = request.PhoneNumber;
            driver.TricycleNumber = request.TricycleNumber;
            driver.IsActive = request.IsActive;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        #endregion Public Methods
    }
}