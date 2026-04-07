using Microsoft.AspNetCore.Identity;
using TP1.Models.Branches;

namespace TP1.Models.ApplicationUsers
{
    public class UserCreate : IdentityUser
    {
        // Lien avec la succursale
        public int? BranchId { get; set; }

        public virtual BranchCreate Branch { get; set; }

        // Dropdown avec toutes les succursales pour lui permettre d'en choisir une 
        // Dropdown avec la liste les rôles pour lui permettre d'en choisir un
    }
}