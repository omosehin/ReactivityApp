using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using reactive.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace reactive.Infrastructure.Security
{
    public class IsHostRequirement :IAuthorizationRequirement
    {

    }

    public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement> // implement the abstract
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;

        public IsHostRequirementHandler(IHttpContextAccessor httpContextAccessor,
            DataContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            IsHostRequirement requirement)
        {
            /* 
                 we want to estalish if a user is a host of a particular activity 
                 criteria
                 >Get the activity id
                 >Also get the user Id as well
                 >then we can get the activity from the db

              */
            if (context.Resource is AuthorizationFilterContext authContext)
            {
                var currentUserName = _httpContextAccessor.HttpContext.User?.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                //  var activityId = Guid.Parse(authContext.RouteData.Values("id").Value.ToString()); //Guid
                var activityId = Guid.Parse(authContext.RouteData.Values["id"].ToString());

                var activity = _context.Activities.FindAsync(activityId).Result;

                var host = activity.UserActivities.FirstOrDefault(x => x.IsHost);

                if (host?.AppUser?.UserName == currentUserName)
                {
                    context.Succeed(requirement);
                }
            }
            else
            {
                context.Fail();
            }

            
            return Task.CompletedTask;
        }
    }

}
