using Entities.General.RideData;

namespace DriverTaxiService.Repository.Interfaces;

public interface IDriveRepository
{
    public Task<string> StartWork(string phoneNumber);
    public Task<string> EndWork(string phoneNumber);
    public Task<List<RideDb>> GetAllAvailableOrders(string phoneNumber);
    public Task<string> TakeOrderById(int phoneNumber, string phoneNumber1);
    public Task<string> EndOrder(string phoneNumber);
}