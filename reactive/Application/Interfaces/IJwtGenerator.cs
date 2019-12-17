using reactive.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reactive.Application.Interfaces
{
   public interface IJwtGenerator
    {
        string CreateToken(AppUser user);
    }
}
