using AdminControlPanel.Repository.Interfaces;
using Database.MockDatabase;
using Entities.CustomerApi.Requests;
using Entities.General.RideData;

namespace AdminControlPanel.Repository.Mock;

public class MockRideAdminRepository : IRideAdminRepository
{
    private IRideAdminRepository _rideAdminRepositoryImplementation;

    public async Task<List<RideDb>> GetAllRidesWithId()
    {
        return MockDatabases.RideList;
    }

    public async Task<string> RemoveRide(int id)
    {
        if (await CheckExist(id) != "Ok") 
            return "user is not exist";
        
        MockDatabases.RideList.Remove(MockDatabases.RideList.FirstOrDefault(x => x.Id == id));
        
        return "Ok";
    }

    private async Task<string> CheckExist(int id)
    {
        var entity = MockDatabases.RideList.FirstOrDefault(x => x.Id == id);
        
        return entity == null ? "user is not exist" : "Ok";
    }

    public async Task<string> AddNewOrder(Order newOrder)
    {
        return await _rideAdminRepositoryImplementation.AddNewOrder(newOrder);
    }

    public async Task<string> CheckRideForExistence(string phoneNumber)
    {
        return await _rideAdminRepositoryImplementation.CheckRideForExistence(phoneNumber);
    }

    public async Task<string> CancelOrder(string phoneNumber)
    {
        return await _rideAdminRepositoryImplementation.CancelOrder(phoneNumber);
    }

    public async Task<Ride?> GetRideInfo(string phoneNumber)
    {
        return await _rideAdminRepositoryImplementation.GetRideInfo(phoneNumber);
    }

    public async Task<List<RideDb>> GetAllRides()
    {
        return await _rideAdminRepositoryImplementation.GetAllRides();
    }
}