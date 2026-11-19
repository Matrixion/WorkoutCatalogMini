using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkoutCatalogMini.Models;
using WorkoutCatalogMini.Data.Entities;
using WorkoutCatalogMini.Data.Enums;
using WorkoutCatalogMini.Data;
using WorkoutCatalogMini.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace WorkoutCatalogMini.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly IExerciseService _service;

        public ExercisesController(IExerciseService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string search, string filter)
        {
            ViewBag.Search = search;
            ViewBag.Filter = filter;

            var exercises = await _service.GetFilteredExercises(search, filter);
            return View(exercises);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Exercise exercise)
        {
            if (exercise.ExerciseName.IsNullOrEmpty())
            {
                ViewBag.Message = "Exercise must have a name.";
                ViewBag.ExerciseName = exercise.ExerciseName;
                ViewBag.MuscleGroup = exercise.MuscleGroup.ToString();
                ViewBag.Difficulty = exercise.Difficulty.ToString();
                ViewBag.Description = exercise.Description;
                return View(exercise);
            }
            if (exercise.RecommendedReps < 1)
            {
                ViewBag.Message = "Recommended Reps must be greater than 0.";
                ViewBag.ExerciseName = exercise.ExerciseName;
                ViewBag.MuscleGroup = exercise.MuscleGroup.ToString();
                ViewBag.Difficulty = exercise.Difficulty.ToString();
                ViewBag.Description = exercise.Description;
                return View(exercise);
            }

            await _service.AddExercise(exercise);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
