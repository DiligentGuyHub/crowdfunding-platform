using crowdfunding_application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.Services
{
    public class TransactionService : GenericService<Transaction>, ITransactionService
    {
        public TransactionService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
