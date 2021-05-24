using crowdfunding_application.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.Services
{
    public class NewsService : GenericService<News>, INewsService
    {
        public NewsService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<News>> GetAllSorted()
        {
            IEnumerable<News> entities = new List<News>(_context.News.OrderByDescending(item => item.CreationDate));
            return entities;
        }
    }
}
