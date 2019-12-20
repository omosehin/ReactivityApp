﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace reactive.Domain
{
    public class AppUser :IdentityUser
    {
        public string DisplayName  { get; set; }
        public string Bio { get; set; }
        public virtual ICollection<UserActivity> UserActivities { get; set; } //virtual to enable lazy loading
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
