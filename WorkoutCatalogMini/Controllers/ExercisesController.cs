using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkoutCatalogMini.Models;

namespace WorkoutCatalogMini.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly ILogger<ExercisesController> _logger;

        public ExercisesController(ILogger<ExercisesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
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
