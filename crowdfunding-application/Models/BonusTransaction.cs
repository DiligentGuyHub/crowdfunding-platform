using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models
{
    public class BonusTransaction : ModelBase
    {
        [ForeignKey("UserId")]
        public string UserId { get; set; }

        [ForeignKey("BonusId")]
        public int BonusId { get; set; }
    }
}
