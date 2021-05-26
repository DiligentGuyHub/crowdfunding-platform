using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.ViewModels
{
    public class BonusesViewModel
    {
        public List<BonusTransaction> BonusTransactions { get; set; }
        public List<Campaign> Campaigns { get; set; }
        public List<Bonus> Bonuses { get; set; }

        public BonusesViewModel()
        {
            BonusTransactions = new List<BonusTransaction>();
            Campaigns = new List<Campaign>();
            Bonuses = new List<Bonus>();
        }
    }
}
