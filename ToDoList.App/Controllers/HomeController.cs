using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDoList.App.Models;

namespace ToDoList.App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return RedirectToAction("Index", "ToDo");
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
