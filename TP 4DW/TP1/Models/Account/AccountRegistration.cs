using LocationManagerCore.Domains;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TP1.Models.Branches;

namespace TP1.Models.Account
{
    public class AccountRegistration 
    {
        [Required] 
        [DisplayName("User Name")]
        public string? UserName { get; set; }

        [Required]
        [NotMapped]
        [DisplayName("FullName")]
        public string? FullName { get; set; }

        [Required]
        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; }

        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)] // ce n'est pas de la validation
        public string? ConfirmPassword { get; set; }

        [Required]
        [DisplayName("Role")]
        public string? Role { get; set; }

        //[Required]
        //[DisplayName("Agree To Terms and Conditions")]
        //public bool AgreeToTerms { get; set; } = false;


        //// Lien avec la succursale
        //public int? BranchId { get; set; }

        ////[Required]
        //[DisplayName("Branch")]
        //public virtual Branch Branch { get; set; }

        // Dropdown avec toutes les succursales pour lui permettre d'en choisir une 
        // Dropdown avec la liste les rôles pour lui permettre d'en choisir un
    }
}