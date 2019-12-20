using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using reactive.Application.Interfaces;
using reactive.Domain;
using reactive.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace reactive.Application.Photos
{
    public class Add
    {
        public class Command : IRequest<Photo> //photo is the return type
        {
            public IFormFile File { get; set; }
        }

        public class Handler : IRequestHandler<Command,Photo>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAcceesor;
            private readonly IPhotoAccessor _photoAccessor;

            public Handler( DataContext context,IUserAccessor userAccessor,IPhotoAccessor photoAccessor )
            {
                _context = context;
                _userAcceesor = userAccessor;
                _photoAccessor = photoAccessor;
            }

            public async Task<Photo> Handle(Command request, CancellationToken cancellationToken)
            {
                var photoUploadResult = _photoAccessor.AddPhoto(request.File);
                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAcceesor.GetCurrentUsername());
                var photo = new Photo
                {
                    Url = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId,
                    
            };

                if (!user.Photos.Any(x => x.IsMain))
                    photo.IsMain = true;

                user.Photos.Add(photo);
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return photo;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
