using CustomerTaxiService.Constants;
using CustomerTaxiService.Repository.Interfaces;
using Entities.CustomerTaxiService.CustomerData;
using Entities.CustomerTaxiService.RideData;

namespace CustomerTaxiService.Repository.MockRepository;

public class MockRideRepository : IRideRepository
{
    private static List<RideDb> _mockRepository = new(); // RideDB
    public async Task<string> AddNewOrder(int userId, string endPoint)
    {
        try
        { 
            _mockRepository.Add( await CreateRideEntityForDb(userId,endPoint)); // model + id of user
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CreateNewOrderConstants.DatabaseProblems;
        }

        return CreateNewOrderConstants.Ok;
    }

    public async Task<string> CheckRideForExistence(string rideId)
    {
        var rideEntity = new RideDb();
        try
        {
            rideEntity = _mockRepository.FirstOrDefault(x => x.Id == rideId);
        }
        catch (Exception e)
        {
            return CheckInformationConstants.DatabaseProblems;
        }

        return rideEntity == null ? CheckInformationConstants.RideNotFound : CheckInformationConstants.Ok;
    }

    private async Task<RideDb?> TakeRideDbEntity(string rideId)
    {
        RideDb rideEntity;
        try
        {
            rideEntity = _mockRepository.FirstOrDefault(x => x.Id == rideId)!;
        }
        catch (Exception e)
        {
            return null;
        }

        return rideEntity;
    }

    public async Task<string> CancelOrder(string rideId)
    {
        var rideEntity = await TakeRideDbEntity(rideId);
        if (rideEntity == null)
            return CheckInformationConstants.DatabaseProblems;
        try
        { 
           _mockRepository.Remove(rideEntity);
        }
        catch (Exception e)
        {
            return CheckInformationConstants.DatabaseProblems;
        }

        return CheckInformationConstants.Ok;
    }

    private async Task<RideDb> CreateRideEntityForDb(int userId, string endPoint)
    {
        return new RideDb
        {
            CustomerId = userId,
            EndPointOfRide = endPoint,
            RideDate = DateTime.Now
        };
    }
}