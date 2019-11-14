namespace Parking.Api.Commands
{
    public class TakeParkingPlaceCommand
    {
        public string ParkingName { get; }
        public int PlaceNumber { get; }

        public TakeParkingPlaceCommand(string parkingName, int placeNumber)
        {
            ParkingName = parkingName;
            PlaceNumber = placeNumber;
        }
    }
}
