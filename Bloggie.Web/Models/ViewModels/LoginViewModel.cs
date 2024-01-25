using System.ComponentModel.DataAnnotations;

namespace Bloggie.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Please enter the correct password")]
        public string Password { get; set; }

        [Required]
        public string  ReturnUrl { get; set; }
    }
}
