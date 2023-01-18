using AdminControlPanel.BL.Interfaces;
using Entities.General.RideData;
using Microsoft.AspNetCore.Mvc;

namespace AdminControlPanel.Controllers;

[ApiController]
public class RideControlPanel : ControllerBase
{
    private readonly IRideLogicForAdmin _rideLogicForAdmin;
    
    public RideControlPanel(IRideLogicForAdmin rideLogicForAdmin)
    {
        _rideLogicForAdmin = rideLogicForAdmin;
    }
    
    [HttpPost("/GetAllRidesWithId")]
    public async Task<List<RideDb>> GetAllRidesWithId()
    {
        return await _rideLogicForAdmin.GetAllRidesWithId();
    }
    
    [HttpPost("/RemoveRideById")]
    public async Task<List<RideDb>> RemoveRideById(int id)
    {
        return await _rideLogicForAdmin.RemoveRideById(id);
    }
}