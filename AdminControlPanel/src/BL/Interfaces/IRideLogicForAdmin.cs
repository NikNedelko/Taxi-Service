using Entities.General;
using Entities.General.RideData;

namespace AdminControlPanel.BL.Interfaces;

public interface IRideLogicForAdmin
{
    public Task<List<RideDb>> GetAllRidesWithId();
    public Task<List<RideDb>> RemoveRideById(int id);
}