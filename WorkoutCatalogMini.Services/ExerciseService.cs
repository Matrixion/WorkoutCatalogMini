using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutCatalogMini.Data;
using WorkoutCatalogMini.Data.Entities;
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

        public async Task<List<Exercise>> GetFilteredExercises(string? search, string? filter)
        {
            IQueryable<Exercise> exercises = _context.Exercises
                .Include(e => e.Difficulty)
                .Include(e => e.MuscleGroups)
                    .ThenInclude(emg => emg.MuscleGroup);

            if (!string.IsNullOrWhiteSpace(search))
            {
                exercises = exercises.Where(e => e.ExerciseName.Contains(search));
            }

            if (Guid.TryParse(filter, out var muscleId))
            {
                exercises = exercises.Where(e => e.MuscleGroups
                    .Any(emg => emg.MuscleGroupId == muscleId));
            }

            return await exercises.OrderBy(e => e.ExerciseName).ToListAsync();
        }

        public async Task<List<MuscleGroup>> GetMuscleGroups()
        {
            return await _context.MuscleGroups
                .OrderBy(m => m.MuscleName)
                .ToListAsync();
        }

        public async Task CreateExercise(string name, string muscle, string difficulty, int reps, string? description)
        {
            var _muscle = await _context.MuscleGroups
                .FirstOrDefaultAsync(m => m.MuscleName == muscle);
            if (_muscle == null)
            {
                _muscle = new MuscleGroup
                {
                    MuscleGroupId = Guid.NewGuid(),
                    MuscleName = muscle
                };
                _context.MuscleGroups.Add(_muscle);
            }

            var _difficulty = await _context.Difficulties
                .FirstOrDefaultAsync(d => d.DifficultyName == difficulty);
            if (_difficulty == null)
            {
                _difficulty = new Difficulty
                {
                    DifficultyId = Guid.NewGuid(),
                    DifficultyName = difficulty
                };
                _context.Difficulties.Add(_difficulty);
            }

            var exercise = new Exercise
            {
                ExerciseId = Guid.NewGuid(),
                ExerciseName = name.Trim(),
                DifficultyId = _difficulty.DifficultyId,
                Difficulty = _difficulty,
                RecommendedReps = reps,
                Description = string.IsNullOrWhiteSpace(description) ? null : description
            };

            exercise.MuscleGroups.Add(new ExerciseMuscleGroup
            {
                ExerciseId = exercise.ExerciseId,
                Exercise = exercise,
                MuscleGroupId = _muscle.MuscleGroupId,
                MuscleGroup = _muscle
            });

            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
        }
    }
}
