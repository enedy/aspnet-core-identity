using System.ComponentModel.DataAnnotations;

namespace AspnetCoreIdentity.Identity.DTOs.Request
{
    public class CreateUserRequestDTO
    {
        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "{0} is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Pwd { get; set; }

        [Compare(nameof(Pwd), ErrorMessage = "Passwords must be the same")]
        public string PwdConfirm { get; set; }
    }
}
