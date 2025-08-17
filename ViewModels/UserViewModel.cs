using Microsoft.AspNetCore.Identity;

namespace MvcFreelan.ViewModels
{
    public class UserViewModel
    {
        public IdentityUser User { get; set;  }
        public List<RoleViewModel> Roles { get; set; }
    }
}
