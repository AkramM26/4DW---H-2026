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
        [StringLength(254, ErrorMessage = "L'adresse courriel ne peut pas dépasser 254 caractères.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Le courriel doit contenir un domaine valide (ex: .com, .ca).")]
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
        [DisplayName("Rôle")]
        public string? Role { get; set; }

        [Display(Name = "Succursale associée")]
        public Guid? BranchId { get; set; }
        // Obligatoire pour Gérant et Commis / validé dans le controller
    }
}