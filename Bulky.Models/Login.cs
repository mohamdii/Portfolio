using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Login
    {
        [Required]
        [Display(Name = "Login using username or email")]
        public string UserNameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
