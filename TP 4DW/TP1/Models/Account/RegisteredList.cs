using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TP1.Models.Account
{
    public class RegisteredList
    {
        [Required]
        [DisplayName("Nom")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Prénom")]
        public string FirstName { get; set; }


        [Required]
        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; init; }

        [Required]
        [DisplayName("Role")]
        public string Role { get; set; }
    }
}
