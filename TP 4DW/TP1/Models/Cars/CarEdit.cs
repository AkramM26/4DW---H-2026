using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TP1.Models.Cars
{
    public class CarEdit
    {
        public  Guid Id { get; set; }

        [Required(ErrorMessage = "Le surnom est obligatoire.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Le surnom doit avoir entre 5 et 20 caractères.")]
        public required string Nickname { get; set; }

        [Required]
        public required string Status { get; set; }

        [Required]
        public required bool Availability { get; set; }

        [Required]
        public required bool State { get; set; }

        [Required(ErrorMessage = "Le NIV est obligatoire.")]
        [RegularExpression(@"^[A-Z0-9]{17}$", ErrorMessage = "Le format du NIV est invalide.")]
        public required string SerialNumber { get; set; }
        [Required(ErrorMessage = "L'immatriculation est obligatoire.")]
        [RegularExpression(@"^[a-zA-Z0-9]{6,7}$", ErrorMessage = "6 à 7 caractères alphanumériques sans espace.")]
        public required string Registration { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ]*$", ErrorMessage = "Lettres seulement, sans espace.")]
        public required string CarBrand { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public required string CarModel { get; set; }

        [Required]
        [Range(2000, 2027, ErrorMessage = "L'année doit être entre 2000 et 2027.")]
        public required int? Year { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public required string Color { get; set; }

        [Range(0, int.MaxValue)]
        public required int? Mileage { get; set; }

        [Range(0, (double)decimal.MaxValue)]
        [DataType(DataType.Currency)]
        public required decimal? EstimatedValue { get; set; }

        public  Guid BranchId { get; set; }

    }
}
