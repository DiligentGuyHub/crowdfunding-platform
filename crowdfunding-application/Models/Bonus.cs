using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models
{
    public class Bonus : ModelBase
    {
        [ForeignKey("CampaignId")]
        public int CampaignId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Money { get; set; }
        public string Image { get; set; }
    }
}
