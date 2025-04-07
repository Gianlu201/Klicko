using Microsoft.AspNetCore.Identity;

namespace Klicko_be.Models.Auth
{
    public class ApplicationRole : IdentityRole
    {
        // navigazione
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
