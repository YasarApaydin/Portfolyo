using Portfolyo.Entities.Common;

namespace Portfolyo.Entities.Models
{
    public class Profile:BaseEntity
    {
  
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Biography { get; set; }
        public string Email { get; set; }
        public string GithubUrl { get; set; }
        public string LinkedInUrl { get; set; }


  
    }
}
