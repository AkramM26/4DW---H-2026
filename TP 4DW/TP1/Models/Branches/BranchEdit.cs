using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TP1.Models.Branches
{
    public class BranchEdit
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Active")]
        public bool Status { get; set; } // Activée ou Désactivée

        [Required(ErrorMessage = "Le nom de la succursale est obligatoire.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Le nom doit avoir entre 5 et 20 caractères.")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ0-9\s\-]+$", ErrorMessage = "Le nom ne peut contenir que des lettres, chiffres, espaces ou tirets.")]
        [Display(Name = "Nom de la succursale")]
        public string Name { get; set; }
    }
}
