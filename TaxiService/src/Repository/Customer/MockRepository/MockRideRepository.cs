using Database.MockDatabase;
using Entities.General;
using Entities.General.RideData;
using TaxiService.Constants.Customer;
using TaxiService.Repository.Customer.Interfaces;

namespace TaxiService.Repository.Customer.MockRepository;

public class MockRideRepository : IRideRepository
{
    public async Task<string> AddNewOrder(string phoneNumber, string endPoint)
    {
        MockDatabases.RideList.Add(await CreateRideEntityForDb(phoneNumber, endPoint));
        return CustomerConstants.Ok;
    }

    public async Task<string> CheckRideForExistence(string phoneNumber)
    {
        var rideEntity = MockDatabases.RideList.FirstOrDefault(x => x.CustomerPhoneNumber == phoneNumber);

        return rideEntity == null ? CustomerConstants.RideNotFound : CustomerConstants.Ok;
    }

    private async Task<RideDb?> TakeRideDbEntity(string phoneNumber)
    {
        var rideEntity = MockDatabases.RideList.FirstOrDefault(x => x.CustomerPhoneNumber == phoneNumber)!;
        return rideEntity;
    }

    public async Task<string> CancelOrder(string phoneNumber)
    {
        var rideEntity = await TakeRideDbEntity(phoneNumber);
        if (rideEntity == null)
            return CustomerConstants.RideNotFound;
        MockDatabases.RideList.Remove(rideEntity);
        return CustomerConstants.Ok;
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
        return MockDatabases.RideList;
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