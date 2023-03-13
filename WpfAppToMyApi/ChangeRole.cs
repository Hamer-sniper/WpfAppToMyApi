
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace WpfAppToMyApi
{
    public class ChangeRole
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeRole()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
