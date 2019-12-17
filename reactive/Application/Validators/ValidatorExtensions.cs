using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reactive.Application.Validators
{
    public static class ValidatorExtensions //static because we are going to be instantisting this class anywhere
    {
public static IRuleBuilder<T,string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                    .NotEmpty()
                    .MinimumLength(6)
                    .WithMessage("Password must be at least 6 characters")
                    .Matches("A-Z")
                    .WithMessage("Password must contain at least 1 uppercase letter")
                    .Matches("a-z")
                    .WithMessage("Password must contain atleast 1 lowercse characters")
                    .Matches("[0-9]")
                    .WithMessage("Password must contain atleast 1 number")
                    .Matches("[^a-zA-z0-9]")
                    .WithMessage("Password must contain non alphanumeric");
            return options;
                    
        }
    }
}
