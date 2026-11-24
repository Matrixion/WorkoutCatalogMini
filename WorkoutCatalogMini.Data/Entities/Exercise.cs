using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutCatalogMini.Data.Entities
{
    public class Exercise
    {
        public Guid ExerciseId { get; set; }
        public string ExerciseName { get; set; }

        public ICollection<ExerciseMuscleGroup> MuscleGroups = new List<ExerciseMuscleGroup>();

        [ForeignKey(nameof(Difficulty))]
        public Guid DifficultyId { get; set; }
        public Difficulty Difficulty { get; set; }

        public int RecommendedReps { get; set; }
        public string Description { get; set; }
    }
}

