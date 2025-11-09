using Portfolyo.Entities.Common;

namespace Portfolyo.Entities.Models
{
    public class Skill:BaseEntity
    {
   
        public string Name { get; set; }
        public int Level { get; set; } 
        public string IconClass { get; set; }
        public string Category { get; set; }

    }
}
