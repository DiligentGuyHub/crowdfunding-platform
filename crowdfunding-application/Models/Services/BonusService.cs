using crowdfunding_application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.Services
{
    public class BonusService : GenericService<Bonus>, IBonusService
    {
        public BonusService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
