using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using reactive.Application.Errors;
using reactive.Application.Interfaces;
using reactive.Application.Validators;
using reactive.Domain;
using reactive.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace reactive.Application.User
{
    public class Register
    {
        public class Command : IRequest<User> //return a user on registration registration//is not a must to return anything
        {
            public string DisplayName { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DisplayName).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).NotEmpty().Password();
            }
        }

        // IRequest is an interface from mediatr
        public class Query : IRequest<Command> { }

        public class Handler : IRequestHandler<Command,User>
        {
            private readonly DataContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(DataContext context,UserManager<AppUser> userManager,IJwtGenerator jwtGenerator)
            {
                _context = context;
                _userManager = userManager;
               _jwtGenerator = jwtGenerator;
            }

            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _context.Users.Where(c => c.Email == request.Email).AnyAsync()) //to check for existing email
                    throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exist" });
               
                if (await _context.Users.Where(c => c.UserName == request.Username).AnyAsync()) //to check for existing email
                    throw new RestException(HttpStatusCode.BadRequest, new { Username = "Username already exist" });

                var user = new AppUser
                {
                    DisplayName = request.DisplayName,
                    Email = request.Email,
                    UserName = request.Username
                };
                
                var result = await _userManager.CreateAsync(user,request.Password);  //create new user

                if (result.Succeeded)
                {
                    return new User
                    {
                        DisplayName = user.DisplayName,
                        Token = _jwtGenerator.CreateToken(user),
                        Username = user.UserName,
                        Image = user.Photos.FirstOrDefault(x => x.IsMain)?.Url

                    };
                }

                throw new Exception("Problem creating user");
            }
        }
    }
}
