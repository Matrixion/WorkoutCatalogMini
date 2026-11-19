using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutCatalogMini.Data;
using WorkoutCatalogMini.Data.Entities;
using WorkoutCatalogMini.Data.Enums;
using WorkoutCatalogMini.Services.Interfaces;

namespace WorkoutCatalogMini.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly AppDbContext _context;

        public ExerciseService(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Exercise>> GetFilteredExercises(string search, string filter)
        {
            IQueryable<Exercise> exercises = _context.Exercises.OrderBy(e => e.ExerciseName);
            if (!string.IsNullOrEmpty(search))
            {
                exercises = exercises.Where(e => e.ExerciseName.ToLower().Contains(search.ToLower()));
            }
            if (filter != "all" && !string.IsNullOrEmpty(filter))
            {
                exercises = exercises
                    .Where(e => e.MuscleGroup.ToString() == filter);
            }
            return exercises.ToListAsync();
        }

        public async Task AddExercise(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
        }
    }
}
