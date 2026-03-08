using LocationManageCore.Domains;
using LocationManagerCore.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP1.Models.Locations
{
    public class LocationCreate
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Display(Name = "Statut")]
        public bool Status { get; set; } // Ouverte ou Fermée

        [Required(ErrorMessage = "La date d'ouverture est obligatoire.")]
        [Display(Name = "Date d'ouverture")]
        [DataType(DataType.DateTime)]
        public DateTime Opening { get; set; }

        [Required(ErrorMessage = "La date de fermeture prévue est obligatoire.")]
        [Display(Name = "Fermeture prévue")]
        [DataType(DataType.DateTime)]
        public DateTime PlanedClosing { get; set; }

        [Display(Name = "Fermeture officielle")]
        [DataType(DataType.DateTime)]
        public DateTime? OfficialClosing { get; set; } // Nullable car pas encore fermée au début


        [Required]
        public Guid CarId { get; set; }
        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }

        [Required]
        public Guid DriverId { get; set; }
        [ForeignKey("DriverId")]
        public virtual Driver Driver { get; set; }

        public virtual ICollection<Note>? Notes { get; set; }
    }
}