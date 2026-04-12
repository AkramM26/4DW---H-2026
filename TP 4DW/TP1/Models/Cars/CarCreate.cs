using LocationManagerCore.Domains;
using Microsoft.CodeAnalysis.Operations;
using Mono.TextTemplating;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace TP1.Models.Cars
{
    public class CarCreate
    {

        [Required(ErrorMessage = "Le surnom est obligatoire.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Le surnom doit avoir entre 5 et 20 caractères.")]
        public required string Nickname { get; set; }

        [Required]
        public required bool Status { get; set; }

        [Required]
        public required bool Availability { get; set; }

        [Required]
        public required bool State { get; set; }

        [Required(ErrorMessage = "Le NIV est obligatoire.")]
        [RegularExpression(@"^[A-Z0-9]{17}$", ErrorMessage = "Le format du NIV est invalide.")]
        public required string SerialNumber { get; set; }
        [Required(ErrorMessage = "L'immatriculation est obligatoire.")]
        [RegularExpression(@"^[a-zA-Z0-9]{6,7}$", ErrorMessage = "L'immatriculation doit avoir 6 ou 7 caractères, sans espace.")]
        public required string Registration { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ]*$", ErrorMessage = "La marque ne doit contenir que des lettres, sans espace.")]
        public required string CarBrand { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ0-9]*$", ErrorMessage = "Le modèle ne doit pas contenir d'espace.")]
        public required string CarModel { get; set; }

        [Required]
        [Range(2000, 2027, ErrorMessage = "L'année doit être entre 2000 et 2027.")]
        public required int Year { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ]*$", ErrorMessage = "La couleur ne doit contenir que des lettres, sans espace.")]
        public required string Color { get; set; }

        [Range(0, int.MaxValue)]
        public required int Mileage { get; set; }

        [Range(0, (double)decimal.MaxValue)]
        [DataType(DataType.Currency)]
        public required decimal EstimatedValue { get; set; }

        public Guid BranchId { get; set; }
    }

    public class CarCreateFactory()
    {
        public static CarCreate Create(Guid branchId)
        {
            return new CarCreate()
            {
                Nickname = string.Empty,
                SerialNumber = string.Empty,
                Registration = string.Empty,
                CarBrand = string.Empty,
                CarModel = string.Empty,
                Color = string.Empty,
                Year = DateTime.Now.Year,
                Status = true,
                Availability = true,
                State = true,
                Mileage = 0,
                EstimatedValue = 0m,
                BranchId = branchId
            };
        }
    }
}
