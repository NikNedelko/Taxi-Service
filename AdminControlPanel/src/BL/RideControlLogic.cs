using AdminControlPanel.BL.Interfaces;
using AdminControlPanel.Repository.Interfaces;
using Entities.General.RideData;
using TaxiService.Repository.Customer.Interfaces;

namespace AdminControlPanel.BL;

public class RideControlLogic : IRideLogicForAdmin
{
    private readonly IRideAdminRepository _rideRepository;

    public RideControlLogic(IRideAdminRepository rideRepository)
    {
        _rideRepository = rideRepository;
    }

    public async Task<List<RideDb>> GetAllRidesWithId()
    {
        return await _rideRepository.GetAllRidesWithId();
    }

    public async Task<List<RideDb>> RemoveRideById(int id)
    {
        throw new NotImplementedException();
    }
}