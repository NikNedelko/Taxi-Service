using Entities.DriverApi;
using Entities.General.RideData;

namespace TaxiService.Repository.Driver.Interfaces;

public interface IDriveRepository
{
    public Task<string> StartWork(string phoneNumber);
    public Task<string> EndWork(string phoneNumber);
    public Task<List<RideDb>> GetAllAvailableOrders(DriveClass driveClass);
    public Task<string> TakeOrderById(int phoneNumber, string phoneNumber1);
    public Task<string> EndOrder(string phoneNumber);
}