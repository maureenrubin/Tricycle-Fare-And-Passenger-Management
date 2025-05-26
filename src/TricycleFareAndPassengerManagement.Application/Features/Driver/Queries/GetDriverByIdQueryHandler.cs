using MediatR;
using Microsoft.EntityFrameworkCore;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Domain.Interfaces;

namespace TricycleFareAndPassengerManagement.Application.Features.Driver.Queries
{
    public class GetDriverByIdQueryHandler : IRequestHandler<GetDriverByIdQuery, DriverDto?>
    {
        #region Fields

        private readonly IAppDbContext _context;

        #endregion Fields

        #region Public Constructors

        public GetDriverByIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<DriverDto?> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Drivers
                .Where(d => d.Id == request.Id)
                .Select(d => new DriverDto
                {
                    Id = d.Id,
                    FullName = d.FullName,
                    LicenseNumber = d.LicenseNumber,
                    PhoneNumber = d.PhoneNumber,
                    TricycleNumber = d.TricycleNumber,
                    DateRegistered = d.DateRegistered,
                    IsActive = d.IsActive
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        #endregion Public Methods
    }
}