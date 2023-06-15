namespace BusStationApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

public class HomeController : Controller
{
    public HomeController()
    {
    }

    public IActionResult Index()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction("All", "Destinations");
        }

        return View();
    }
}
