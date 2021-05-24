using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models
{
    public class Campaign : ModelBase
    {
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string HomeImage { get; set; }
        public string Images { get; set; }
        public bool IsFinished { get; set; }
        public int MoneyGoal { get; set; }
        public int MoneyActual { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<Bonus> Bonuses { get; set; }
        public ICollection<News> News { get; set; }
        public ICollection<PaymentTransaction> Transactions { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }

}
