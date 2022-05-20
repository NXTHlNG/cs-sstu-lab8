using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace cs_sstu_lab8.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        public List<Order> Orders { get; set; }
    }
}