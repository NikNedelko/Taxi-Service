using Entities.General;
using Entities.General.RideData;
using Microsoft.AspNetCore.Mvc;

namespace AdminControlPanel.Controllers;

[ApiController]
public class RideControlPanel : ControllerBase
{
    public async Task<List<RideDb>> GetAllRidesWithId()
    {
        
    }
    
    public async Task<List<RideDb>> RemoveRide(int id)
    {
        
    }
}