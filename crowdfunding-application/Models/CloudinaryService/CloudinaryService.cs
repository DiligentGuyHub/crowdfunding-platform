﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.CloudinaryService
{
    public class CloudinaryService : ICloudinaryService
    {
        public CloudinaryService(CloudinaryConfiguration configuration)
        {
            _account = new Account(configuration.CloudName, configuration.CloudApiKey, configuration.CloudApiSecret);
            _cloudinary = new Cloudinary(_account);
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
    }
}
