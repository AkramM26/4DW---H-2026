using Microsoft.AspNetCore.Identity;

namespace TP1.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Lien avec la succursale
        public int? BranchId { get; set; }
        public virtual Branch Branch { get; set; }
    }
}