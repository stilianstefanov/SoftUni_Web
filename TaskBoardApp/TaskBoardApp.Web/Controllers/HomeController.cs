﻿using Microsoft.AspNetCore.Mvc;

namespace TaskBoardApp.Web.Controllers;

public class HomeController : Controller
{
    public HomeController(ILogger<HomeController> logger)
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}
