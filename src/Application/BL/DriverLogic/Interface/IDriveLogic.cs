
using Domain.Entities.General;
using Domain.Entities.General.RideData;

namespace Application.BL.DriverLogic.Interface;

public interface IDriveLogic
{
    public Task<Response> StartWork(string phoneNumber);
    public Task<Response> EndWork(string phoneNumber);
    public Task<List<RideDb>> GetAllAvailableOrders(string phoneNumber);
    public Task<Response> TakeOrderById(int rideId, string phoneNumber);
    public Task<Response> EndOrder(string phoneNumber);
}