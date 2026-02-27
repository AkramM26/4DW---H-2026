using System.ComponentModel.DataAnnotations;

namespace TP1.Models
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Le surnom est obligatoire.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Le surnom doit avoir entre 5 et 20 caractères.")]
        public string Nickname { get; set; } // Exemple: Car#5

        [Required]
        public bool Status { get; set; } // Activée ou Désactivée

        [Required]
        public bool Availability { get; set; } // Disponible ou Indisponible

        [Required]
        public bool State { get; set; } // Neuf ou Usagé

        [Required(ErrorMessage = "Le NIV est obligatoire.")]
        [RegularExpression(@"^[A-Z0-9]{17}$", ErrorMessage = "Le format du NIV est invalide.")]
        public string SerialNumber { get; set; } // Format standard NIV

        [Required]
        [StringLength(7, MinimumLength = 6)]
        public string Registration { get; set; } // Immatriculation

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string CarBrand { get; set; } // Marque

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string CarModel { get; set; } // Modèle

        [Required]
        [Range(2000, 2027, ErrorMessage = "L'année doit être entre 2000 et 2027.")]
        public int Year { get; set; } // Entre 2000 et l'année prochaine

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Color { get; set; } // Couleur

        [Range(0, int.MaxValue)]
        public int Mileage { get; set; } // Entier positif

        [Range(0, (double)decimal.MaxValue)]
        [DataType(DataType.Currency)]
        public decimal EstimatedValue { get; set; } // Nombre décimal positif


        public int? BranchId { get; set; }
        public virtual Branch Branch { get; set; }
    }
}