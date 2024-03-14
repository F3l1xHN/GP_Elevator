
namespace Elevator.Model.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Elevator : IElevator
    {
        public bool DoorsOpened { get; set; }

        public int MaxNumberOfPersons { get; set; }

        public void CloseDoors() => DoorsOpened = false;

        public void OpenDoors() => DoorsOpened = true;
    }
}
