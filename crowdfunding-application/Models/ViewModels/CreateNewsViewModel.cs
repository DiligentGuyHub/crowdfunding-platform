using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.ViewModels
{
    public class CreateNewsViewModel
    {
        public News News { get; set; }
        public int CampaignId { get; set; }
        public IFormFile MainImage
        {
            get;
            set;
        }
    }
}
