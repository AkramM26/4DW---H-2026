using System.ComponentModel.DataAnnotations;

namespace TP1.Models.Locations
{
    public class LocationDelete
    {

        [Display(Name = "Fermeture officielle")]
        [DataType(DataType.DateTime)]
        public DateTime? OfficialClosing { get; set; }



        public string? Note { get; set; }
    }
}
