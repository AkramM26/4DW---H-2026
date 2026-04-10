using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TP1.Models.Account
{
    public class LoginViewModel
    {
        [HiddenInput]
        public string? ReturnUrl { get; set; } = null;

        [DisplayName("User Name")]
        public string? Username { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }


        [DisplayName("RememberMe")]
        public bool RememberMe { get; set; } = false;
    }
}