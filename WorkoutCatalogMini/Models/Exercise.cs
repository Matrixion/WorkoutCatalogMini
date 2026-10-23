namespace WorkoutCatalogMini.Models
{
    public class Exercise
    {
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
        public Difficulty Difficulty { get; set; }
        public int RecommendedReps { get; set; }
        public string Description { get; set; }
    }
}

