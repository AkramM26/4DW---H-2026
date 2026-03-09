using LocationManagerCore.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP1.Models.Drivers
{
    public class DriverCreate
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Le nom doit avoir entre 3 et 50 caractères.")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s\-]*$", ErrorMessage = "Le nom ne doit contenir que des lettres.")]
        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Le prénom doit avoir entre 3 et 30 caractères.")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s\-]*$", ErrorMessage = "Le prénom ne doit contenir que des lettres.")]
        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Format de courriel invalide.")]
        [Display(Name = "Courriel")]
        public string EmailAdress { get; set; }

        [Required(ErrorMessage = "Le numéro de téléphone est obligatoire.")]
        [RegularExpression(@"^(\(\d{3}\)\s\d{3}-\d{4}|\d{3}-\d{3}-\d{4}|\d{10})$",
            ErrorMessage = "Formats acceptés : (555) 555-5555, 555-555-5555 ou 5555555555.")]
        [Display(Name = "Téléphone")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Le numéro de permis est obligatoire.")]
        [RegularExpression(@"^[A-Z]\d{4}-\d{6}-\d{2}$", ErrorMessage = "Format invalide (Ex: A1234-121299-12).")]
        [Display(Name = "Numéro de permis")]
        public string DriverLicenceNumber { get; set; }

        public Guid AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address? Address { get; set; }

        // Propriété de navigation pour les locations
        public virtual ICollection<Location>? Locations { get; set; }
    }
}