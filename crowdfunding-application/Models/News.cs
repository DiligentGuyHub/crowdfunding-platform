﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models
{
    public class News : ModelBase
    {
        public int CampaignId { get; set; }
        [ForeignKey("CampaignId")]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string Image { get; set; }
    }
}
