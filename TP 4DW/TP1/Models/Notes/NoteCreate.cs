using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP1.Models.Notes
{
    public class NoteCreate
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le contenu de la note est obligatoire.")]
        [StringLength(500, ErrorMessage = "La note ne peut pas dépasser 500 caractères.")]
        [Display(Name = "Contenu")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Date de création")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}