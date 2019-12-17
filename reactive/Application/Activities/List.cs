using MediatR;
using Microsoft.EntityFrameworkCore;
using reactive.Domain;
using reactive.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace reactive.Application.Activities
{
    public class List
    {
        // IRequest is an interface from mediatr
        public class Query : IRequest<List<Activity>> { }

        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<Activity>> Handle(Query request, 
                CancellationToken cancellationToken)
            {
                var activities = await _context.Activities
                    .Include(c=>c.UserActivities)
                    .ThenInclude(c=>c.AppUser)
                    .ToListAsync();

                return activities;
            }
        }
    }
}
