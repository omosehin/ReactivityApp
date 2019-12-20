using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using reactive.Application.Interfaces;
using reactive.Application.Photos;
using reactive.Infrastructure.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reactive.Infrastructure.Photos
{
    public class PhotoAccessor : IPhotoAccessor
    {
        private readonly Cloudinary _cloudinary;
        public PhotoAccessor(IOptions<CloudinarySettings> config) //IOptions is used to access out secret key
        {
           /* var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                ); */

            var acc = new Account(  //the secret key was not working i have to manually set it here
                "abvideo",
                "424791299756342",
                "LjAZzmC4rdw0lDEUxv8beo4nzA4"
                );

            _cloudinary = new Cloudinary(acc);
        }

        public PhotoUploadResult AddPhoto(IFormFile file)
        {
            var uploadResult = new ImageUploadResult(); //

            if (file.Length > 0) //check if file is not empty
            {
                //read file into memory
                using(var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face") //optional this can be done in the front end
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            if (uploadResult.Error !=null)
            {
                throw new Exception(uploadResult.Error.Message);
            }

            return new PhotoUploadResult
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.SecureUri.AbsoluteUri
            };
        }

        public string DeletePhoto(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = _cloudinary.Destroy(deleteParams);

            return result.Result == "ok" ? result.Result : null;
        }
    }
}
