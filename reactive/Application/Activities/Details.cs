using MediatR;
using Microsoft.EntityFrameworkCore;
using reactive.Application.Errors;
using reactive.Domain;
using reactive.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace reactive.Application.Activities
{
    public class Details
    {
        public class Query : IRequest<Activity>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Activity>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Activity> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var activity = await _context.Activities
                    .Include(x=>x.UserActivities)
                    .ThenInclude(x=>x.AppUser)
                    .SingleOrDefaultAsync(x=>x.Id==request.Id);

                if (activity == null)
                    throw new RestException(HttpStatusCode.NotFound, new 
                    { activity = "Not found" });

                return activity;
            }
        }
    }

}
