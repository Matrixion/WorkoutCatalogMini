using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkoutCatalogMini.Models;
using WorkoutCatalogMini.Data.Entities;
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

            ViewBag.MuscleGroups = await _service.GetMuscleGroups();

            var exercises = await _service.GetFilteredExercises(search, filter);
            return View(exercises);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string? name, string? muscle, string? difficulty, int reps, string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
                ViewBag.Message = "Exercise must have a name.";
            else if (string.IsNullOrWhiteSpace(muscle))
                ViewBag.Message = "Please enter a muscle group.";
            else if (string.IsNullOrWhiteSpace(difficulty))
                ViewBag.Message = "Please enter a difficulty level.";
            else if (reps < 1)
                ViewBag.Message = "Recommended reps must be greater than 0.";

            if (ViewBag.Message != null)
            {
                ViewBag.ExerciseName = name;
                ViewBag.MuscleGroup = muscle;
                ViewBag.Difficulty = difficulty;
                ViewBag.RecommendedReps = reps;
                ViewBag.Description = description;
                return View();
            }

            await _service.CreateExercise(name!, muscle!, difficulty!, reps, description);
            return RedirectToAction(nameof(Index));
        }
    }
}
