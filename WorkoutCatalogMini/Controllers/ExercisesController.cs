using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkoutCatalogMini.Models;
using WorkoutCatalogMini.Data.Entities;
using WorkoutCatalogMini.Data.Enums;
using WorkoutCatalogMini.Data;

namespace WorkoutCatalogMini.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly AppDbContext _context;

        public ExercisesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string search, string filter)
        {
            if (!_context.Exercises.Any())
            {
                _context.Exercises.Add(new Exercise { ExerciseName = "Push-up", MuscleGroup = MuscleGroup.Chest, Difficulty = Difficulty.Easy, RecommendedReps = 10, Description = "A basic upper body exercise that targets the chest, shoulders, and triceps." });
                _context.Exercises.Add(new Exercise { ExerciseName = "Squat", MuscleGroup = MuscleGroup.Quads, Difficulty = Difficulty.Easy, RecommendedReps = 15, Description = "A lower body exercise that targets the quadriceps, hamstrings, and glutes." });
                _context.Exercises.Add(new Exercise { ExerciseName = "Plank", MuscleGroup = MuscleGroup.Abs, Difficulty = Difficulty.Medium, RecommendedReps = 30, Description = "An isometric core exercise that strengthens the abdominal muscles and lower back." });
                _context.SaveChanges();
            }
            var exercises = _context.Exercises.ToList();

            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;

                exercises = exercises
                    .Where(e => e.ExerciseName.ToLower().Contains(search.ToLower()))
                    .ToList();
            }
            if (filter != "all" && !string.IsNullOrEmpty(filter))
            {
                ViewBag.Filter = filter;

                exercises = exercises
                    .Where(e => e.MuscleGroup.ToString() == filter)
                    .ToList();
            }
            return View(exercises);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Exercise exercise)
        {
            if (exercise.RecommendedReps < 1)
            {
                ViewBag.Message = "Recommended Reps must be greater than 0.";
                ViewBag.ExerciseName = exercise.ExerciseName;
                ViewBag.MuscleGroup = exercise.MuscleGroup.ToString();
                ViewBag.Difficulty = exercise.Difficulty.ToString();
                ViewBag.Description = exercise.Description;
                return View(exercise);
            }

            _context.Exercises.Add(exercise);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
