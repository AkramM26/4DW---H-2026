using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TP1.Models.Account
{
    public class LoginViewModel
    {
        [HiddenInput]
        public string? ReturnUrl { get; set; } = null;

        [Required]
        [DisplayName("User Name")]
        [StringLength(254, ErrorMessage = "Nom d'utilisateur ou mot de passe invalide")]
        public string? Username { get; set; }

        [Required]
        [DisplayName("Password")]
        [StringLength(128, ErrorMessage = "Nom d'utilisateur ou mot de passe invalide")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }


        [DisplayName("RememberMe")]
        public bool RememberMe { get; set; } = false;
    }
}