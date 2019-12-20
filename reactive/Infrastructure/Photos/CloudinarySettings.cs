using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reactive.Infrastructure.Photos
{
    public class CloudinarySettings
    {
        //this must match the names we set in out secret keys
        public string CloudName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }
}
