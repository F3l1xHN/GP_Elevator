namespace Elevator.Model.Entities
{
    public abstract class ControlPanel
    {
        public int CurrentElevatorPosition { get; set; }
        public int TargetFloor { get; set; }
        public Direction ElevatorDirectionMovement { get; set; }
        protected IElevator Elevator { get; set; } 
        protected int NumberOfFloors { get; set; }
        public List<Request> Requests { get; set; }

        protected abstract void PurgeRequests(List<Request> requests);
        protected abstract void MoveElevatorUpwards();
        protected abstract void MoveElevatorDownwards();
        protected abstract void StopElevator();

    }
}
