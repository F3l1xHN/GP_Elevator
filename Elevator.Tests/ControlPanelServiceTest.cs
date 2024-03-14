using Elevator.Model.Entities;
using Elevator.Service;
using Moq;

namespace Elevator.Tests
{
    public class ControlPanelServiceTest
    {
        private int numberOfFloors = 5;

        [Fact]
        public void ReceiveRequest_AddRequests_ExpectedNumberOfRequests()
        {
            //Arrange
            int currentElevatorPosition = 1;
            var elevator = new Model.Entities.Elevator() { MaxNumberOfPersons = 8, DoorsOpened = false };
            var controlPanelService = new ControlPanelService(elevator, numberOfFloors, currentElevatorPosition, Direction.STANDBY);
            var request = new List<Request>() {
                new Request() { Direction = Direction.UPWARDS, FloorNumber = 5 },
                new Request() { Direction = Direction.DOWNWARDS, FloorNumber = 2 }
            };

            //Act
            request.ForEach(r => controlPanelService.ReceiveRequest(r));
            var requestResutl = controlPanelService.Requests.Count;

            //Assert
            Assert.Equal(2, requestResutl);
        }

        [Fact]
        public void DispatchRequests_HigherFloor_ExpectedElevatorPosition()
        {
            //Arrange
            var elevator = new Model.Entities.Elevator() { MaxNumberOfPersons = 8, DoorsOpened = false };
            var controlPanelService = new ControlPanelService(elevator, 5, 1, Direction.STANDBY);
            var request = new Request() { Direction = Direction.UPWARDS, FloorNumber = 3 };
            controlPanelService.ReceiveRequest(request);

            //Act
            controlPanelService.DispatchRequests();
            var result = controlPanelService.CurrentElevatorPosition;

            //Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void DispatchRequests_LowerFloor_ExpectedElevatorPosition()
        {
            //Arrange
            var elevator = new Model.Entities.Elevator() { MaxNumberOfPersons = 8, DoorsOpened = false };
            var controlPanelService = new ControlPanelService(elevator, 5, 5, Direction.STANDBY);
            var request = new Request() { Direction = Direction.UPWARDS, FloorNumber = 2 };
            controlPanelService.ReceiveRequest(request);

            //Act
            controlPanelService.DispatchRequests();
            var result = controlPanelService.CurrentElevatorPosition;

            //Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void DispatchRequests_MultipleRequestOnTheSameWayFromTopToBottom_ExpectedElevatorPosition()
        {
            //Arrange
            int currentElevatorPosition = 5;
            var elevator = new Model.Entities.Elevator() { MaxNumberOfPersons = 8, DoorsOpened = false };
            var controlPanelService = new ControlPanelService(elevator, numberOfFloors, currentElevatorPosition, Direction.STANDBY);
            var request = new List<Request>() {
                new Request() { Direction = Direction.DOWNWARDS, FloorNumber = 1 },
                new Request() { Direction = Direction.DOWNWARDS, FloorNumber = 4 },
                new Request() { Direction = Direction.UPWARDS, FloorNumber = 3 },
                new Request() { Direction = Direction.UPWARDS, FloorNumber = 2 }
            }; 
            request.ForEach(r => controlPanelService.ReceiveRequest(r));

            //Act
            controlPanelService.DispatchRequests();
            var requestResutl = controlPanelService.Requests;
            var currentPositionResult = controlPanelService.CurrentElevatorPosition;

            //Assert
            Assert.Equal(3, currentPositionResult);
            Assert.Empty(requestResutl);
        }

        [Fact]
        public void DispatchRequests_MultipleRequestOnTheSameWayFromBottomToTop_ExpectedElevatorPosition()
        {
            //Arrange
            int currentElevatorPosition = 1;
            var elevator = new Model.Entities.Elevator() { MaxNumberOfPersons = 8, DoorsOpened = false };
            var controlPanelService = new ControlPanelService(elevator, numberOfFloors, currentElevatorPosition, Direction.STANDBY);
            var request = new List<Request>() {
                new Request() { Direction = Direction.UPWARDS, FloorNumber = 5 },
                new Request() { Direction = Direction.DOWNWARDS, FloorNumber = 4 },
                new Request() { Direction = Direction.UPWARDS, FloorNumber = 3 },
                new Request() { Direction = Direction.DOWNWARDS, FloorNumber = 2 }
            };
            request.ForEach(r => controlPanelService.ReceiveRequest(r));

            //Act
            controlPanelService.DispatchRequests();
            var requestResutl = controlPanelService.Requests;
            var currentPositionResult = controlPanelService.CurrentElevatorPosition;

            //Assert
            Assert.Equal(2, currentPositionResult);
            Assert.Empty(requestResutl);
        }

        [Fact]
        public void DispatchRequests_AttendSameRequestFromDifferentFloor_ExpectedElevatorPosition()
        {
            //Arrange
            int currentElevatorPosition = 1;
            var elevator = new Model.Entities.Elevator() { MaxNumberOfPersons = 8, DoorsOpened = false };
            var controlPanelService = new ControlPanelService(elevator, numberOfFloors, currentElevatorPosition, Direction.STANDBY);
            var request = new List<Request>() {
                new Request() { Direction = Direction.DOWNWARDS, FloorNumber = 5 },
                new Request() { Direction = Direction.DOWNWARDS, FloorNumber = 5 },
                new Request() { Direction = Direction.DOWNWARDS, FloorNumber = 5 }
            };
            request.ForEach(r => controlPanelService.ReceiveRequest(r));

            //Act
            controlPanelService.DispatchRequests();
            var requestResutl = controlPanelService.Requests;
            var currentPositionResult = controlPanelService.CurrentElevatorPosition;

            //Assert
            Assert.Equal(5, currentPositionResult);
            Assert.Empty(requestResutl);
        }
    }
}
