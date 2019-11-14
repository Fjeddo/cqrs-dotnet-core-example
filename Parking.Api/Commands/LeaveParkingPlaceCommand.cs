namespace Parking.Api.Commands
{
    public class LeaveParkingPlaceCommand
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
