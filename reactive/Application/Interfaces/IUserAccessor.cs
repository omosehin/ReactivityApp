﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reactive.Application.Interfaces
{
    public interface IUserAccessor
    {
        string GetCurrentUsername();
    }
}
