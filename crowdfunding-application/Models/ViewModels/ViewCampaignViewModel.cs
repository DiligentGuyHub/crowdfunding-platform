using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.ViewModels
{
    public class ViewCampaignViewModel
    {
        public ViewCampaignViewModel()
        {
            Campaigns = new List<Campaign>();
        }

        public List<Campaign> Campaigns { get; set; }
        public string Category { get; set; }
    }
}
