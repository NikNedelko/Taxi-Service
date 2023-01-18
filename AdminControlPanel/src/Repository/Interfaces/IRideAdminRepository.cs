using Entities.General.RideData;

namespace AdminControlPanel.Repository.Interfaces;

public interface IRideAdminRepository
{
    public Task<List<RideDb>> GetAllRidesWithId();
    public Task<List<RideDb>> RemoveRide(int id);
}