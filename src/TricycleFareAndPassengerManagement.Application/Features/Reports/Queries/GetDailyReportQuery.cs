using MediatR;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;

namespace TricycleFareAndPassengerManagement.Application.Features.Reports.Queries
{
    public class GetDailyReportQuery : IRequest<DailyReportDto>
    {
        #region Properties

        public DateTime Date { get; set; } = DateTime.Today;

        #endregion Properties
    }
}