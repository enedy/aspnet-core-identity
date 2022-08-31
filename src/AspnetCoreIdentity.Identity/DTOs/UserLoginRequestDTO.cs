using System.ComponentModel.DataAnnotations;

namespace AspnetCoreIdentity.Identity.DTOs
{
    public class UserLoginRequestDTO
    {
        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "{0} is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Pwd { get; set; }
    }
}
