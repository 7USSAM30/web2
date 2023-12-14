using Microsoft.AspNetCore.Mvc;
using project.Models;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net.Mail;

namespace project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Warning()
        {
            return View();
        }
        
        

        public async Task<IActionResult> Index()
        {
            ViewData["role"] = HttpContext.Session.GetString("Role");
            return View();

        }

       

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}