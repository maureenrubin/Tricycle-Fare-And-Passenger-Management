using MediatR;

namespace TricycleFareAndPassengerManagement.Application.Features.Driver.Commands
{
    public class DeleteDriverCommand : IRequest<bool>
    {
        #region Properties

        public int Id { get; set; }

        #endregion Properties
    }
}