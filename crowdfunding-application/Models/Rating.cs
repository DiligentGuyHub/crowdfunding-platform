using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models
{
    public class Rating : ModelBase
    {
        [ForeignKey("UserId")]
        public string UserId { get; set; }

        [ForeignKey("CampaignId")]
        public int CampaignId { get; set; }
        public int Score { get; set; }
    }
}
