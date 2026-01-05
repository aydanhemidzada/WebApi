using Microsoft.AspNetCore.Identity;

namespace WebApiConfigurationn.Entities.Auth
{
    public class AppUser<Guid>:IdentityUser
    {
        public string FullName { get; set; }
    }
}
