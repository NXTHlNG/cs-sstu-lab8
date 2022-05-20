using Microsoft.AspNetCore.Identity;

namespace cs_sstu_lab8.ViewModels
{
    public class ChangeRolesModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeRolesModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
