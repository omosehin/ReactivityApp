using AutoMapper;
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
        public class Query : IRequest<List<ActivityDto>> { }

        public class Handler : IRequestHandler<Query, List<ActivityDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<ActivityDto>> Handle(Query request, 
                CancellationToken cancellationToken)
            {
                var activities = await _context.Activities
                    .Include(c=>c.UserActivities)
                    .ThenInclude(c=>c.AppUser)
                    .ToListAsync();

                return _mapper.Map<List<Activity>,List<ActivityDto>>(activities);
            }
        }
    }
}
