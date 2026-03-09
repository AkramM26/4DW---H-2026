using LocationManagerCore.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP1.Models.Drivers
{
    public class DriverCreate
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Format de courriel invalide.")]
        [Display(Name = "Courriel")]
        public string EmailAdress { get; set; }

        [Required]
        [Phone(ErrorMessage = "Format de téléphone invalide.")]
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