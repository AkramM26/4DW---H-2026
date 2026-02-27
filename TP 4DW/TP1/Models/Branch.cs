using System.ComponentModel.DataAnnotations;

namespace TP1.Models
{
    public class Branch
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }

        public string Address { get; set; }

        public bool IsActive { get; set; } = true;

        // Propriétés de navigation
        public virtual ICollection<Car> Cars { get; set; }
    }
}