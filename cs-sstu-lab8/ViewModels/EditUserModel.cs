using System.ComponentModel.DataAnnotations;

namespace cs_sstu_lab8.ViewModels
{
    public class EditUserModel
    {
        public string Id;

        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }
    }
}
