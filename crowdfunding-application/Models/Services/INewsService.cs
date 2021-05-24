using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.Services
{
    public interface INewsService : IGenericService<News>
    {
        Task<IEnumerable<News>> GetAllSorted();

    }
}
