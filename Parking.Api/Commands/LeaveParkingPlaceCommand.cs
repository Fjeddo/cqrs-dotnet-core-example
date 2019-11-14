namespace Parking.Api.Commands
{
    public class LeaveParkingPlaceCommand : ICommand
    {
        public string ParkingName { get; }
        public int PlaceNumber { get; }

        public LeaveParkingPlaceCommand(string parkingName, int placeNumber)
        {
            ParkingName = parkingName;
            PlaceNumber = placeNumber;
        }
    }
}
