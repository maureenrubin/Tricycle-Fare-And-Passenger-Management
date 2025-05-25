using MediatR;
using Microsoft.EntityFrameworkCore;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Domain.Interfaces;

namespace TricycleFareAndPassengerManagement.Application.Features.Passengers.Queries
{
    public class GetAllPassengersQueryHandler : IRequestHandler<GetAllPassengersQuery, List<PassengerDto>>
    {
        #region Fields

        private readonly IAppDbContext _context;

        #endregion Fields

        #region Public Constructors

        public GetAllPassengersQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<List<PassengerDto>> Handle(GetAllPassengersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Passengers
                .Select(p => new PassengerDto
                {
                    Id = p.Id,
                    FullName = p.FullName,
                    PhoneNumber = p.PhoneNumber,
                    DateRegistered = p.DateRegistered,
                    IsActive = p.IsActive
                })
                .OrderBy(p => p.FullName)
                .ToListAsync(cancellationToken);
        }

        #endregion Public Methods
    }
}