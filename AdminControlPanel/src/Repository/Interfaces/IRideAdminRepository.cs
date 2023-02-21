using Entities.General.RideData;
using TaxiService.Repository.Customer.Interfaces;

namespace AdminControlPanel.Repository.Interfaces;

public interface IRideAdminRepository : IRideRepository
{
    public Task<List<RideDb>> GetAllRidesWithId();
    public Task<string> RemoveRide(int id);
}