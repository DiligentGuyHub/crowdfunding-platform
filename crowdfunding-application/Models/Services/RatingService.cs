using crowdfunding_application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.Services
{
    public class RatingService : GenericService<Rating>, IRatingService
    {
        public RatingService(ApplicationDbContext context) : base(context)
        {

        }
    }
}
