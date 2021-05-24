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
            BonusList = new List<Bonus>();
        }

        public Campaign campaign { get; set; }
        public List<News> NewsList { get; set; }
        public List<Bonus> BonusList { get; set; }

    }
}
