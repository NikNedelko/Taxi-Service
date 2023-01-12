using Microsoft.AspNetCore.Mvc;

namespace AdminControlPanel.Controllers;

[ApiController]
public class RideControlPanel : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}