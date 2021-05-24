using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.ViewModels
{
    public class TransactionsViewModel
    {
        public List<PaymentTransaction> Transactions { get; set; }
        public TransactionsViewModel()
        {
            Transactions = new List<PaymentTransaction>();
        }
    }
}
