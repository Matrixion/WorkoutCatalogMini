using WorkoutCatalogMini.Data.Entities;

namespace WorkoutCatalogMini.Services.Interfaces
{
    public interface IExerciseService
    {
        Task<List<Exercise>> GetFilteredExercises(string search, string filter);
        Task CreateExercise(string name, string muscle, string difficulty, int reps, string? description);
        Task<List<MuscleGroup>> GetMuscleGroups();
    }
}
