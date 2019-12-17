using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reactive.Domain
{
    public class UserActivity
    {
        public string AppUserId { get; set; } //FK

        public AppUser AppUser { get; set; }

        public Guid ActivityId { get; set; } //FK

        public DateTime DateJoined { get; set; }
        public bool IsHost { get; set; }
        public Activity Activity { get; set; }
    }
}
