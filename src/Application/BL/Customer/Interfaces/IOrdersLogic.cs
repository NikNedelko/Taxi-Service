using Domain.Entities.CustomerApi.Requests;
using Domain.Entities.General;
using Domain.Entities.General.RideData;

namespace Application.BL.Customer.Interfaces;

public interface IOrdersLogic
{
    public Task<Response> BeginNewOrder(Order order);
    public Task<Response> CancelOrder(string phoneNumber);
    public Task<Ride?> GetRideInfo(string phoneNumber);
    public Task<List<RideDb>> GetAllRides();
    
}