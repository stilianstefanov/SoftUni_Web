namespace BusStationApp.Controllers;

using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction("All", "Destinations");
        }

        return View();
    }
}
