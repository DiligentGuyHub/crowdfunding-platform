using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.CloudinaryService
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IWebHostEnvironment _appEnvironment;
        public CloudinaryService(CloudinaryConfiguration configuration, IWebHostEnvironment appEnvironment)
        {
            _account = new Account(configuration.CloudName, configuration.CloudApiKey, configuration.CloudApiSecret);
            _cloudinary = new Cloudinary(_account);
            _appEnvironment = appEnvironment;
        }

        private Account _account {get;set;}
        private Cloudinary _cloudinary { get; set; }

        public ImageUploadResult UploadImage(string imagePath)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imagePath)
            };
            var uploadResult = _cloudinary.Upload(uploadParams);
            return uploadResult;
        }

        public ImageUploadResult UploadImage(IFormFile uploadedFile)
        {
            // путь к папке Files
            string path = uploadedFile.FileName;
            // сохраняем файл в папку Files в каталоге wwwroot
            using (var fileStream = new FileStream(Path.Combine(_appEnvironment.WebRootPath, path), FileMode.Create))
            {
                uploadedFile.CopyTo(fileStream);
            }

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(Path.Combine(_appEnvironment.WebRootPath, path))
            };
            var uploadResult = _cloudinary.Upload(uploadParams);
            return uploadResult;
        }
    }
}
