using LocationManagerCore.Domains;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TP1.Models.Cars;

namespace TP1.Models.Drivers
{
    public class DriverCreate
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
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
        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        [StringLength(254, ErrorMessage = "L'adresse courriel ne peut pas dépasser 254 caractères.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Le courriel doit contenir un domaine valide (ex: .com, .ca).")]
        [EmailAddress(ErrorMessage = "Le format de l'adresse courriel n'est pas valide.")]
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

        [NotMapped]
        public Guid AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address? Address { get; set; }

        // Propriété de navigation pour les locations
        public virtual ICollection<Location>? Locations { get; set; }
    }

    public class DriverCreateFactory()
    {
        public static DriverCreate Create(Guid AddressId)
        {
            return new DriverCreate()
            {
                AddressId = AddressId,
                FirstName= string.Empty,
                LastName= string.Empty,
                EmailAdress= string.Empty,  
                PhoneNumber= string.Empty,
                DriverLicenceNumber= string.Empty,
                Locations= new List<Location>() { }
            };
        }
    }
}