using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.ViewModels
{
    public class InboxCampaignViewModel
    {
        public List<Campaign> Campaigns { get; set; }

        public InboxCampaignViewModel()
        {
            Campaigns = new List<Campaign>();
        }
    }
}
