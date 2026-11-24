using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutCatalogMini.Data.Entities
{
    public class ExerciseMuscleGroup
    {
        public Guid ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public Guid MuscleGroupId { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
    }
}
