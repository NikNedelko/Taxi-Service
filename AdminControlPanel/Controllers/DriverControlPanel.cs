using Microsoft.AspNetCore.Mvc;

namespace AdminControlPanel.Controllers;

[ApiController]
public class DriverControlPanel : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}