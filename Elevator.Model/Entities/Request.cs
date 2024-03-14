namespace Elevator.Model.Entities
{
    public class Request
    {
        public Direction Direction { get; set; }
        public int FloorNumber { get; set; }
    }

    public enum Direction
    {
        UPWARDS, 
        DOWNWARDS,
        STANDBY
    }
}
