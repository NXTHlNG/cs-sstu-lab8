using System.ComponentModel.DataAnnotations;

namespace cs_sstu_lab8.ViewModels
{
    public class LoginModel
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}