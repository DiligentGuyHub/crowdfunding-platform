using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models
{
    public class Transaction : ModelBase
    {
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public int CampaignId { get; set; }
        [ForeignKey("CampaignId")]

        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
