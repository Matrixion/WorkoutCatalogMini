using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutCatalogMini.Data.Entities
{
    public class Difficulty
    {
        public Guid DifficultyId { get; set; }
        public string DifficultyName { get; set; }
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
