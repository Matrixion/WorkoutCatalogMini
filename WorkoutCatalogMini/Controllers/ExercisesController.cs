using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkoutCatalogMini.Models;

namespace WorkoutCatalogMini.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly ILogger<ExercisesController> _logger;
        private static List<Exercise> exercises = new List<Exercise>
        {
            new Exercise { ExerciseId = 1, ExerciseName = "Push-up", MuscleGroup = MuscleGroup.Chest, Difficulty = Difficulty.Easy, RecommendedReps = 10, Description = "A basic upper body exercise that targets the chest, shoulders, and triceps." },
            new Exercise { ExerciseId = 2, ExerciseName = "Squat", MuscleGroup = MuscleGroup.Quads, Difficulty = Difficulty.Easy, RecommendedReps = 15, Description = "A lower body exercise that targets the quadriceps, hamstrings, and glutes." },
            new Exercise { ExerciseId = 3, ExerciseName = "Plank", MuscleGroup = MuscleGroup.Abs, Difficulty = Difficulty.Medium, RecommendedReps = 30, Description = "An isometric core exercise that strengthens the abdominal muscles and lower back." }
        };
        private static int nextId = 4;

        public ExercisesController(ILogger<ExercisesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string search, string filter)
        {
            var filteredExercises = exercises;

            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;

                filteredExercises = filteredExercises
                    .Where(e => e.ExerciseName.ToLower().Contains(search.ToLower()))
                    .ToList();
            }
            if (filter != "all" && !string.IsNullOrEmpty(filter))
            {
                ViewBag.Filter = filter;

                filteredExercises = filteredExercises
                    .Where(e => e.MuscleGroup.ToString() == filter)
                    .ToList();
            }
            return View(filteredExercises);
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

            exercise.ExerciseId = nextId++;
            exercises.Add(exercise);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
