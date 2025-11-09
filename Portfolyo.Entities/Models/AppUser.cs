using Microsoft.AspNetCore.Identity;

namespace Portfolyo.Entities.Models
{
    public sealed class AppUser: IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string LastName { get; set; }

    }
}
