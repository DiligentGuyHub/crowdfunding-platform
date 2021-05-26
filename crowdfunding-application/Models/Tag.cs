﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models
{
    public class Tag : ModelBase
    {
        [ForeignKey("CampaignId")]
        public int CampaingId { get; set; }
        public string Title { get; set; }
    }
}
