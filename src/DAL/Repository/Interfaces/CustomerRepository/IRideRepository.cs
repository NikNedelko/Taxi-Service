using Domain.Entities.CustomerData.Requests;
using Domain.Entities.RideData;

namespace DAL.Repository.Interfaces.CustomerRepository;

public interface IRideRepository
{
    public Task<string> AddOrderToDatabase(OrderModel newOrder);
    public Task<string> CheckRideForExistence(string phoneNumber);
    public Task<string> CancelOrder(string phoneNumber);
    public Task<Ride?> GetRideInfo(string phoneNumber);
    public Task<List<RideDb>> GetAllRides();
}