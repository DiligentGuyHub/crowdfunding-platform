using crowdfunding_application.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.IO;

namespace crowdfunding_application.Models.ViewModels
{
    public class CreateCampaignViewModel
    {
        public Campaign Campaign { get; set; }
        public IFormFile MainImage { get;
            set; }
        public string ImagePath { get; set; }
        
    }
}
