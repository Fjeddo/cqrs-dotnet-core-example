namespace Parking.Api.Commands
{
    public class CreateParkingCommand : ICommand
    {
        public string ParkingName { get; }
        public int Capacity { get; }

        public CreateParkingCommand(string parkingName, int capacity)
        {
            ParkingName = parkingName;
            Capacity = capacity;
        }
    }
}
