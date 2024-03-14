using Elevator.Model.Entities;

namespace Elevator.Service.Abstractions
{
    public interface IControlPanelService
    {
        void DispatchRequests();
        void ReceiveRequest(Request request);
        void TurnOff();
        void TurnOn();
    }
}
