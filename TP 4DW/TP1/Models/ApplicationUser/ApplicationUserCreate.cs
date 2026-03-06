using Microsoft.AspNetCore.Identity;
using TP1.Models.Branch;

namespace TP1.Models.ApplicationUser
{
    public class ApplicationUserCreate : IdentityUser
    {
        // Lien avec la succursale
        public int? BranchId { get; set; }

        public virtual BranchCreate Branch { get; set; }

    }
}