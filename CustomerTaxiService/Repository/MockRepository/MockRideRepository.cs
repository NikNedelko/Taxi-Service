using CustomerTaxiService.Constants;
using CustomerTaxiService.Repository.Interfaces;
using Entities.CustomerTaxiService.CustomerData;
using Entities.CustomerTaxiService.RideData;
using Entities.General;

namespace CustomerTaxiService.Repository.MockRepository;

public class MockRideRepository : IRideRepository
{
    private static List<RideDb> _mockRepository = new()
    {
        new RideDb
        {
            Id = 1,
            CustomerPhoneNumber = "1234",
            EndPointOfRide = "Heaven",
            RideDate = DateTime.Today,
            DriverFeedBack = (int)FeedBack.Normal,
            CustomerFeedBack = (int)FeedBack.Normal
        }
    };

    public async Task<string> AddNewOrder(string phoneNumber, string endPoint)
    {
        try
        {
            _mockRepository.Add(await CreateRideEntityForDb(phoneNumber, endPoint));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CreateNewOrderConstants.DatabaseProblems;
        }

        return CreateNewOrderConstants.Ok;
    }

    public async Task<string> CheckRideForExistence(string phoneNumber)
    {
        var rideEntity = new RideDb();
        try
        {
            rideEntity = _mockRepository.FirstOrDefault(x => x.CustomerPhoneNumber == phoneNumber );
        }
        catch
        {
            return CheckInformationConstants.DatabaseProblems;
        }

        return rideEntity == null ? CheckInformationConstants.RideNotFound : CheckInformationConstants.Ok;
    }

    private async Task<RideDb?> TakeRideDbEntity(string phoneNumber)
    {
        RideDb rideEntity;
        try
        {
            rideEntity = _mockRepository.FirstOrDefault(x => x.CustomerPhoneNumber == phoneNumber)!;
        }
        catch
        {
            return null;
        }

        return rideEntity;
    }

    public async Task<string> CancelOrder(string phoneNumber)
    {
        var rideEntity = await TakeRideDbEntity(phoneNumber);
        if (rideEntity == null)
            return CheckInformationConstants.RideNotFound;
        try
        {
            _mockRepository.Remove(rideEntity);
        }
        catch
        {
            return CheckInformationConstants.DatabaseProblems;
        }

        return CheckInformationConstants.Ok;
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
        return _mockRepository;
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