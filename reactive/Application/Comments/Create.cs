using AutoMapper;
using MediatR;
using reactive.Application.Activities;
using reactive.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static reactive.Application.Activities.Create;

namespace reactive.Application.Comments
{
    public class Create
    {
        public class Command : IRequest
        {
            public string Body { get; set; }
            public Guid ActivityId { get; set; }
            public string Username  { get; set; }

        }
    }

    public class Handler : IRequestHandler<CommandDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler( DataContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommentDto> Handle(CommandDto request, CancellationToken cancellationToken)
        {
            var activity = _context.Activities.FindAsync(request.)

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem creating comment");
        }
    }

}
