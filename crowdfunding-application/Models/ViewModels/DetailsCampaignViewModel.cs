using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.ViewModels
{
    public class DetailsCampaignViewModel
    {
        public DetailsCampaignViewModel()
        {
            NewsList = new List<News>();
        }

        public Campaign campaign { get; set; }
        public List<News> NewsList { get; set; }

    }
}
