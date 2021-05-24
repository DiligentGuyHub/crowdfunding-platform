using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.ViewModels
{
    public class CreateBonusViewModel
    {
        public Bonus Bonus { get; set; }
        public int CampaignId { get; set; }
        public IFormFile MainImage
        {
            get;
            set;
        }
    }
}
