using System.ComponentModel.DataAnnotations;

namespace UbbRentalBike.ViewModels
{
    public class EditUserViewModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "The UserName field is required.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "The Email field is not a valid email address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
    }
}