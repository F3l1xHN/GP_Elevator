using Elevator.Model;
using Elevator.Model.Entities;
using Elevator.Service.Abstractions;

namespace Elevator.Service
{
    public sealed class ControlPanelService : ControlPanel, IControlPanelService
    { 
        public ControlPanelService(  IElevator elevator
                                    , int numberOfFloors = 5
                                    , int currentElevatorPosition = 1
                                    , Direction elevatorDirectionMovement = Direction.STANDBY) 
        {
            Elevator = elevator;
            NumberOfFloors = numberOfFloors;
            TargetFloor = -1;
            CurrentElevatorPosition = currentElevatorPosition;
            ElevatorDirectionMovement = elevatorDirectionMovement;
            Requests = new List<Request>();
        }
        public void ReceiveRequest(Request request) => Requests.Add(request);

        public void DispatchRequests()
        {
            while (Requests.Count > 0)
            {
                var request = Requests.First();
                Requests.Remove(request);
                TargetFloor = request.FloorNumber;
                do
                {
                    var requestsForCurrentFloor = Requests.Where(r => r.Direction == request.Direction 
                                                                    && r.FloorNumber == CurrentElevatorPosition).ToList();
                    if (requestsForCurrentFloor.Count > 0)
                    {
                        StopElevator();
                        PurgeRequests(requestsForCurrentFloor);
                    }
                    if (TargetFloor > CurrentElevatorPosition)
                    {
                        MoveElevatorUpwards();
                    }
                    if (TargetFloor < CurrentElevatorPosition)
                    {
                        MoveElevatorDownwards();
                    }


                } while (TargetFloor != CurrentElevatorPosition);
                TargetFloor = -1;                        
            }
            StopElevator();
        }

        protected override void PurgeRequests(List<Request> requests)
        {
            foreach (var item in requests)
            {
                Requests.Remove(item);
            };
        }

        protected override void StopElevator()
        {
            ElevatorDirectionMovement = Direction.STANDBY;
            Elevator.OpenDoors();
            //Thread.Sleep(1000); To simulate doors movement
            Elevator.CloseDoors();
        }

        protected override void MoveElevatorDownwards() 
        {
            if (CurrentElevatorPosition > 1)
            {
                CurrentElevatorPosition -= 1;
                ElevatorDirectionMovement = Direction.DOWNWARDS;
            }
        }

        protected override void MoveElevatorUpwards()
        {
            if (CurrentElevatorPosition < NumberOfFloors) {
                CurrentElevatorPosition += 1;
                ElevatorDirectionMovement = Direction.UPWARDS;
            }
        }

        public void TurnOff()
        {
            // Apply persistence
            ElevatorDirectionMovement = Direction.STANDBY;
            //Save serialized object
            throw new NotImplementedException();
        }

        public void TurnOn()
        {
            // Apply persistence
            //Read serialized object
            ElevatorDirectionMovement = Direction.STANDBY;
            throw new NotImplementedException();
        }
    }
}
