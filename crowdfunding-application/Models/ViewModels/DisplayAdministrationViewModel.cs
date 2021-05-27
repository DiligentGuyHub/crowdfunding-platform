using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.ViewModels
{
    public class DisplayAdministrationViewModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        public List<IdentityUser> Users { get; set; }
        public List<IdentityUser> Administrators { get; set; }
        public List<IdentityUser> Blocked { get; set; }
        public DisplayAdministrationViewModel()
        {
            Users = new List<IdentityUser>();
            Administrators = new List<IdentityUser>();
            Blocked = new List<IdentityUser>();
        }
    }
}
