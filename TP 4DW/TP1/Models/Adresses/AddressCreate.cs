using System.ComponentModel.DataAnnotations;

namespace TP1.Models.Adresses
{
    public class AddressCreate
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Le numéro civique est obligatoire.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Le numéro civique doit être un entier positif.")]
        [Display(Name = "Numéro civique")]
        public string StreetNumber { get; set; }

        [Required(ErrorMessage = "La rue est obligatoire.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Le nom de rue doit avoir entre 5 et 30 caractères.")]
        [Display(Name = "Rue")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "La ville est obligatoire.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "La ville doit avoir entre 5 et 30 caractères.")]
        [Display(Name = "Ville")]
        public string CityName { get; set; }

        [Required]
        public string Province { get; set; } = "Québec"; // Toujours Québec

        [Required]
        public string Country { get; set; } = "Canada"; // Toujours Canada

        [Required(ErrorMessage = "Le code postal est obligatoire.")]
        [RegularExpression(@"^[A-Z]\d[A-Z] ?\d[A-Z]\d$", ErrorMessage = "Format invalide (A0A 0A0).")]
        [Display(Name = "Code postal")]
        public string PostalCode { get; set; }
    }
}