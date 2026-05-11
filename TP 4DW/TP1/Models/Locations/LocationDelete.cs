using System.ComponentModel.DataAnnotations;

namespace TP1.Models.Locations
{
    public class LocationDelete
    {
        [Required(ErrorMessage = "La date de fermeture réelle est obligatoire.")]
        [Display(Name = "Date et heure de fermeture réelle")]
        [DataType(DataType.DateTime)]
        public DateTime OfficialClosing { get; set; } = DateTime.Now;

        [Display(Name = "Note (optionnel)")]
        [StringLength(500)]
        public string? Note { get; set; }

        // Données passées en hidden pour la validation et la redirection
        public Guid LocationId { get; set; }
        public DateTime Opening { get; set; }
    }
}
