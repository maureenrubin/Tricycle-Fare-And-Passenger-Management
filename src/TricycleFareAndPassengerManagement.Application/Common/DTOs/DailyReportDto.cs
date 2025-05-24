namespace TricycleFareAndPassengerManagement.Application.Common.DTOs
{
    public class DailyReportDto
    {
        #region Properties

        public DateTime Date { get; set; }
        public int TotalTrips { get; set; }
        public decimal TotalRevenue { get; set; }
        public int ActiveDrivers { get; set; }
        public int ActivePassengers { get; set; }

        #endregion Properties
    }
}