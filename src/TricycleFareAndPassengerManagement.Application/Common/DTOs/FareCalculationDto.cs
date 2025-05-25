namespace TricycleFareAndPassengerManagement.Application.Common.DTOs
{
    public class FareCalculationDto
    {
        #region Properties

        public double Distance { get; set; }
        public decimal BaseFare { get; set; } = 15.00m; // Base fare
        public decimal PerKmRate { get; set; } = 8.50m; // Rate per kilometer
        public decimal TotalFare { get; set; }

        #endregion Properties
    }
}