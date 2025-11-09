using Portfolyo.Entities.Common;

namespace Portfolyo.Entities.Models
{
    public class Experience: BaseEntity
    {

        public string CompanyName { get; set; }

        public string Position { get; set; }

        public string Sector { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string WorkingMethod { get; set; }

        public string Explanation { get; set; }

        public string TechnologyUsed { get; set; }
    }
}
