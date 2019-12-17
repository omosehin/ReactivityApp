using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace reactive.Domain
{
    public class AppUser :IdentityUser
    {
        public string DisplayName  { get; set; }
        public ICollection<UserActivity> UserActivities { get; set; }
    }
}
