using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using reactive.Application.Profiles;

namespace reactive.Controllers
{
    public class ProfilesController :BaseController
    {
        [HttpGet ("{username}")]
        public async Task<ActionResult<Profile>> Get(string username)
        {
            return await Mediator.Send(new Details.Query { Username = username });
        }

        [HttpPut ("{username}")]

        public async Task <ActionResult<Unit>> Edit(Edit.Command command )
        {
          
            return await Mediator.Send(command);
        }


    }
}