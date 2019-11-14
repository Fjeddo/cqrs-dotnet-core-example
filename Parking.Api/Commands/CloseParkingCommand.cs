namespace Parking.Api.Commands
{
    public class CloseParkingCommand : ICommand
    {
        public string ParkingName { get; }

        public CloseParkingCommand(string parkingName)
        {
            ParkingName = parkingName;
        }
    }
}
