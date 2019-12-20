using Microsoft.AspNetCore.Http;
using reactive.Application.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reactive.Application.Interfaces
{
   public interface IPhotoAccessor
    {
        PhotoUploadResult AddPhoto(IFormFile file);

        string DeletePhoto(string publicId);
    }
}
