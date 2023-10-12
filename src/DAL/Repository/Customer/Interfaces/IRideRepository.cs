using Domain.Entities.CustomerApi.Requests;
using Domain.Entities.General.RideData;

namespace DAL.Repository.Customer.Interfaces;

public interface IRideRepository
{
    public Task<string> AddOrderToDatabase(OrderEntity newOrder);
    public Task<string> CheckRideForExistence(string phoneNumber);
    public Task<string> CancelOrder(string phoneNumber);
    public Task<Ride?> GetRideInfo(string phoneNumber);
    public Task<List<RideDb>> GetAllRides();
}