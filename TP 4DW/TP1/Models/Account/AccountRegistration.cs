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
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Le nom d'utilisateur doit avoir entre 5 et 20 caractères.")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+$", ErrorMessage = "Le nom d'utilisateur ne doit contenir que des lettres, chiffres, tirets ou underscores.")]
        public string? UserName { get; set; }

        [Required]
        [NotMapped]
        [DisplayName("FullName")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Le nom complet doit avoir entre 5 et 50 caractères.")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s\-]*$", ErrorMessage = "Le nom complet ne peut contenir que des lettres, tirets ou espaces.")]
        public string? FullName { get; set; }

        [Required]
        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Le format de l'adresse courriel n'est pas valide.")]
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