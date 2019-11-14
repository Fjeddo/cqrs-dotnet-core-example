namespace Parking.Api.Queries
{
    public class GetParkingInfoQuery
    {
        public string ParkingName { get; }

        public GetParkingInfoQuery(string parkingName)
        {
            ParkingName = parkingName;
        }
    }
}
