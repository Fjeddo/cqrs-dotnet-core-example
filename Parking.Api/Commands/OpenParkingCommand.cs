namespace Parking.Api.Commands
{
    public class OpenParkingCommand
    {
        public string ParkingName { get; }

        public OpenParkingCommand(string parkingName)
        {
            ParkingName = parkingName;
        }
    }
}
