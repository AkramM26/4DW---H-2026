using LocationManageCore.Domains;
using LocationManagerCore.Domains;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TP1.Models.Cars;

namespace TP1.Models.Locations
{
    public class LocationCreate
    {
        //[NotMapped]
        //[Key]
        //public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Display(Name = "Statut")]
        public bool Status { get; set; } // Ouverte ou Fermée

        [Required(ErrorMessage = "La date d'ouverture est obligatoire.")]
        [Display(Name = "Date d'ouverture")]
        [Range(typeof(DateTime), "2000-01-01", "2050-12-31", ErrorMessage = "La date doit être comprise entre l'an 2000 et 2050.")]
        [DataType(DataType.DateTime)]
        public DateTime Opening { get; set; }

        [Required(ErrorMessage = "La date de fermeture prévue est obligatoire.")]
        [Display(Name = "Fermeture prévue")]
        [Range(typeof(DateTime), "2000-01-01", "2050-12-31", ErrorMessage = "La date doit être comprise entre l'an 2000 et 2050.")]
        [DataType(DataType.DateTime)]
        public DateTime PlanedClosing { get; set; }

        [Display(Name = "Fermeture officielle")]
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "2000-01-01", "2050-12-31", ErrorMessage = "La date doit être comprise entre l'an 2000 et 2050.")]
        public DateTime? OfficialClosing { get; set; } // Nullable car pas encore fermée au début


        [Required]
        public Guid CarId { get; set; }
        [ForeignKey("CarId")]
        [ValidateNever]
        public virtual Car Car { get; set; }


        //Informations concernant le conducteur 
        [NotMapped]
        [Required]
        public Guid DriverId { get; set; }
        [ForeignKey("DriverId")]
        [ValidateNever]
        public virtual Driver Driver { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Le nom doit avoir entre 3 et 50 caractères.")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s\-]*$", ErrorMessage = "Le nom ne doit contenir que des lettres.")]
        [Display(Name = "Nom")]
        public required string DriverLastname{ get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Le prénom doit avoir entre 3 et 30 caractères.")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s\-]*$", ErrorMessage = "Le prénom ne doit contenir que des lettres.")]
        [Display(Name = "Prénom")]
        public required string DriverFirstName { get; set; }


        [Required]
        [DisplayName("Driver's Email Address")]
        [DataType(DataType.EmailAddress)]
        [StringLength(254, ErrorMessage = "L'adresse courriel ne peut pas dépasser 254 caractères.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Le courriel doit contenir un domaine valide (ex: .com, .ca).")]
        [EmailAddress(ErrorMessage = "Le format de l'adresse courriel n'est pas valide.")]
        public string? DriverEmailAddress { get; set; }



        [Required(ErrorMessage = "Le numéro de téléphone est obligatoire.")]
        [RegularExpression(@"^(\(\d{3}\)\s\d{3}-\d{4}|\d{3}-\d{3}-\d{4}|\d{10})$",
            ErrorMessage = "Formats acceptés : (555) 555-5555, 555-555-5555 ou 5555555555.")]
        [DisplayName("Driver's Phone number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Le numéro de permis est obligatoire.")]
        [RegularExpression(@"^[A-Z]\d{4}-\d{6}-\d{2}$", ErrorMessage = "Format invalide (Ex: A1234-121299-12).")]
        [Display(Name = "Driver Licence Number")]
        public string DriverLicenceNumber { get; set; }



        public virtual ICollection<Note>? Notes { get; set; }


        //Address
        [Required(ErrorMessage = "Le numéro civique est obligatoire.")]
        [Range(1, int.MaxValue, ErrorMessage = "Le numéro civique doit être un entier positif.")]
        [Display(Name = "Numéro civique")]
        public string StreetNumber { get; set; }

        [Required(ErrorMessage = "La rue est obligatoire.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Le nom de rue doit avoir entre 5 et 30 caractères.")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s\-]*$", ErrorMessage = "La rue ne doit contenir que des lettres.")]
        [Display(Name = "Rue")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "La ville est obligatoire.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "La ville doit avoir entre 5 et 30 caractères.")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s\-]*$", ErrorMessage = "La ville ne doit contenir que des lettres.")]
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

        //BrandID
        public Guid BranchId { get; set; }

        [StringLength(500, ErrorMessage = "La note ne peut pas dépasser 500 caractères.")]
        [Display(Name = "Note initiale (optionnel)")]
        public string? InitialNote { get; set; }
    }


    public class LocationCreateFactory()
    {
        public static LocationCreate Create(Guid BranchId)
        {
            return new LocationCreate()
            {
                Status = true,
                Opening = DateTime.Now,
                PlanedClosing = DateTime.Now, 
                OfficialClosing = null,

                CarId = Guid.Empty,


                DriverId = Guid.Empty,

                DriverLastname = string.Empty,
                DriverFirstName = string.Empty,
                DriverEmailAddress = string.Empty,
                PhoneNumber = string.Empty,
                DriverLicenceNumber = string.Empty,
                Notes = new List<Note>(),
                StreetNumber = string.Empty,
                StreetName = string.Empty,
                CityName = string.Empty,
                Province = "Québec",
                Country = "Canada",
                PostalCode = string.Empty,
                BranchId = BranchId
            };
        }
    }
}