namespace Parking.Api.Commands
{
    public class OpenParkingCommand : ICommand
    {
        public string ParkingName { get; }

        public OpenParkingCommand(string parkingName)
        {
            ParkingName = parkingName;
        }
    }
}
