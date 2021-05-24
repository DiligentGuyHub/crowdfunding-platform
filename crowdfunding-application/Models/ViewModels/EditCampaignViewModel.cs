using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.ViewModels
{
    public class EditCampaignViewModel
    {
        public Campaign Campaign { get; set; }
        public IFormFile MainImage { get; 
            set; }

    }
}
