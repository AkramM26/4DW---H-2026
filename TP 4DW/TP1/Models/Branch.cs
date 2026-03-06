using LocationManageCore.Domains;
using System.ComponentModel.DataAnnotations;

namespace TP1.Models
{
    public class Branch
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Display(Name = "Active")]
        public bool Status { get; set; } // Activée ou Désactivée

        [Required(ErrorMessage = "Le nom de la succursale est obligatoire.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Le nom doit avoir entre 5 et 20 caractères.")]
        [Display(Name = "Nom de la succursale")]
        public string Name { get; set; }


        public virtual ICollection<Car>? Cars { get; set; }
        public virtual ICollection<ApplicationUser>? Employees { get; set; }

        // Propriétés de navigation
        public static Branch Create(bool status, string name)
        {
            return new Branch
            {
                Id = Guid.NewGuid(),
                Status = status,
                Name = name
            };
        }

        protected Branch()
        {
            Id = Guid.NewGuid();
        }
    }
}