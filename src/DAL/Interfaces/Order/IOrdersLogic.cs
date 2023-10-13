using Domain.Entities.CustomerData.Requests;
using Domain.Entities.General;
using Domain.Entities.RideData;

namespace DAL.Interfaces.Order;

public interface IOrdersLogic
{
    public Task<Response> BeginNewOrder(OrderModel order);
    public Task<Response> CancelOrder(string phoneNumber);
    public Task<Ride?> GetRideInfo(string phoneNumber);
    public Task<List<RideDb>> GetAllRides();
    
}