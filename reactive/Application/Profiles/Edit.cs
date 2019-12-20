﻿using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using reactive.Application.Errors;
using reactive.Application.Interfaces;
using reactive.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using static reactive.Application.Profiles.Edit;

namespace reactive.Application.Profiles
{
    public class Edit
    {
        public class Command : IRequest
        {
            
            public string DisplayName { get; set; }
            public string Bio { get; set; }
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.DisplayName).NotEmpty();
           
        }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        public Handler( DataContext context,IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetCurrentUsername());

            if (user == null)
                throw new RestException(HttpStatusCode.NotFound, new { Profile = "Not found" });

            user.Bio = request.Bio ?? user.Bio;
            user.DisplayName = request.DisplayName ?? user.DisplayName;

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes");

        }
    }
}
