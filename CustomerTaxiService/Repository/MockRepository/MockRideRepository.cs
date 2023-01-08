using CustomerTaxiService.Constants;
using CustomerTaxiService.Repository.Interfaces;
using Database.MockDatabase;
using Entities.General;
using Entities.General.RideData;

namespace CustomerTaxiService.Repository.MockRepository;

public class MockRideRepository : IRideRepository
{
    public async Task<string> AddNewOrder(string phoneNumber, string endPoint)
    {
        MockRideDatabase.RideList.Add(await CreateRideEntityForDb(phoneNumber, endPoint));
        return OrdersConstants.Ok;
    }

    public async Task<string> CheckRideForExistence(string phoneNumber)
    {
        var rideEntity = MockRideDatabase.RideList.FirstOrDefault(x => x.CustomerPhoneNumber == phoneNumber);

        return rideEntity == null ? OrdersConstants.RideNotFound : OrdersConstants.Ok;
    }

    private async Task<RideDb?> TakeRideDbEntity(string phoneNumber)
    {
        var rideEntity = MockRideDatabase.RideList.FirstOrDefault(x => x.CustomerPhoneNumber == phoneNumber)!;
        return rideEntity;
    }

    public async Task<string> CancelOrder(string phoneNumber)
    {
        var rideEntity = await TakeRideDbEntity(phoneNumber);
        if (rideEntity == null)
            return OrdersConstants.RideNotFound;
        MockRideDatabase.RideList.Remove(rideEntity);
        return OrdersConstants.Ok;
    }

    public async Task<Ride?> GetRideInfo(string phoneNumber)
    {
        var rideEntity = await TakeRideDbEntity(phoneNumber);
        if (rideEntity == null)
            return null;
        return await ConvertRideDbToRide(rideEntity);
    }

    public async Task<List<RideDb>> GetAllRides()
    {
        return MockRideDatabase.RideList;
    }

    private async Task<RideDb> CreateRideEntityForDb(string phoneNumber, string endPoint)
    {
        return new RideDb
        {
            CustomerPhoneNumber = phoneNumber,
            EndPointOfRide = endPoint,
            RideDate = DateTime.Now
        };
    }

    private async Task<Ride?> ConvertRideDbToRide(RideDb rideDb)
    {
        return new Ride
        {
            CustomerPhoneNumber = rideDb.CustomerPhoneNumber,
            EndPointOfRide = rideDb.EndPointOfRide,
            RideDate = rideDb.RideDate,
            DriverFeedBack = (FeedBack)rideDb.DriverFeedBack,
            CustomerFeedBack = (FeedBack)rideDb.CustomerFeedBack
        };
    }
}