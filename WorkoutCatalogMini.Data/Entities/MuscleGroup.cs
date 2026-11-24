using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutCatalogMini.Data.Entities
{
    public class MuscleGroup
    {
        public Guid MuscleGroupId { get; set; }
        public string MuscleName { get; set; }
        public ICollection<ExerciseMuscleGroup> Exercises = new List<ExerciseMuscleGroup>();
    }
}
