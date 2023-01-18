using AdminControlPanel.BL.Interfaces;
using Entities.General.RideData;

namespace AdminControlPanel.BL;

public class RideControlLogic : IRideLogicForAdmin
{
    public async Task<List<RideDb>> GetAllRidesWithId()
    {
        throw new NotImplementedException();
    }

    public async Task<List<RideDb>> RemoveRideById(int id)
    {
        throw new NotImplementedException();
    }
}