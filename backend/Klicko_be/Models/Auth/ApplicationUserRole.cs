using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Klicko_be.Models.Auth
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        // navigazione
        [ForeignKey(nameof(UserId))]
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey(nameof(RoleId))]
        public ApplicationRole ApplicationRole { get; set; }
    }
}
