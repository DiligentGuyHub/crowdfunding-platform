using crowdfunding_application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.Services
{
    public class BonusTransactionService : GenericService<BonusTransaction>, IBonusTransactionService
    {
        public BonusTransactionService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
