using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models
{
    public class Comment : ModelBase
    {
        [ForeignKey("CampaignId")]
        public int CampaignId { get; set; }

        //[ForeignKey("UserId")]
        //public string UserId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
    }
}
