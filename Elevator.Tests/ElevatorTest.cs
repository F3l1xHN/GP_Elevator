
namespace Elevator.Tests
{
    public class ElevatorTest
    {
        [Fact]
        public void OpenDoors_True()
        {
            //Arrange
            var elevator = new Model.Entities.Elevator() { DoorsOpened = false, MaxNumberOfPersons = 8 };

            //Act
            elevator.OpenDoors();
            var result = elevator.DoorsOpened;

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void OpenDoors_AlreadyOpened_True() {
            //Arrange
            var elevator = new Model.Entities.Elevator() { DoorsOpened = true, MaxNumberOfPersons = 8 };
            
            //Act
            elevator.OpenDoors();
            var result = elevator.DoorsOpened;

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void CloseDoors_False()
        {
            //Arrange
            var elevator = new Model.Entities.Elevator() { DoorsOpened = true, MaxNumberOfPersons = 8 };

            //Act
            elevator.CloseDoors();
            var result = elevator.DoorsOpened;

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void CloseDoors_AlreadyClosed_False()
        {
            //Arrange
            var elevator = new Model.Entities.Elevator() { DoorsOpened = false, MaxNumberOfPersons = 8 };

            //Act
            elevator.CloseDoors();
            var result = elevator.DoorsOpened;

            //Assert
            Assert.False(result);
        }
    }
}
