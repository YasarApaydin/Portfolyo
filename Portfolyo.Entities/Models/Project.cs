using Portfolyo.Entities.Common;

namespace Portfolyo.Entities.Models
{
    public class Project:BaseEntity
    {
      
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string GithubUrl { get; set; }

        public string Technologies { get; set; } 
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
