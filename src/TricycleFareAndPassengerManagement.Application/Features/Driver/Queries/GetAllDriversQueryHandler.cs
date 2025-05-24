using MediatR;
using Microsoft.EntityFrameworkCore;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Domain.Interfaces;

namespace TricycleFareAndPassengerManagement.Application.Features.Driver.Queries
{
    public class GetAllDriversQueryHandler : IRequestHandler<GetAllDriversQuery, List<DriverDto>>
    {
        #region Fields

        private readonly IAppDbContext _context;

        #endregion Fields

        #region Public Constructors

        public GetAllDriversQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<List<DriverDto>> Handle(GetAllDriversQuery request, CancellationToken cancellationToken)
        {
            return await _context.Drivers
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
                .OrderBy(d => d.FullName)
                .ToListAsync(cancellationToken);
        }

        #endregion Public Methods
    }
}