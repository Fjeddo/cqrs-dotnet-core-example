namespace Parking.Api.Commands
{
    public class CloseParkingCommand
    {
        public string ParkingName { get; }

        public CloseParkingCommand(string parkingName)
        {
            ParkingName = parkingName;
        }
    }
}
