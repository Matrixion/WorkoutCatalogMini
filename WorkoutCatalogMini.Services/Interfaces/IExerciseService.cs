using WorkoutCatalogMini.Data.Entities;
using WorkoutCatalogMini.Data.Enums;

namespace WorkoutCatalogMini.Services.Interfaces
{
    public interface IExerciseService
    {
        Task<List<Exercise>> GetFilteredExercises(string search, string filter);
        Task AddExercise(Exercise exercise);
    }
}
