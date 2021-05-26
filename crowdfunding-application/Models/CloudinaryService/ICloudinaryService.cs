using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.CloudinaryService
{
    public interface ICloudinaryService
    {
        ImageUploadResult UploadImage(string imagePath);
        ImageUploadResult UploadImage(IFormFile formFile);

    }
}
