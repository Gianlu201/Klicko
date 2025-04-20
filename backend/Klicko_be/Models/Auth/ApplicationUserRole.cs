using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Klicko_be.Models.Auth
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        [Key]
        public Guid UserRoleId { get; set; }
        public override required string UserId { get; set; }
        public override required string RoleId { get; set; }

        // navigazione
        [ForeignKey(nameof(UserId))]
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey(nameof(RoleId))]
        public ApplicationRole ApplicationRole { get; set; }
    }
}
