using crowdfunding_application.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.ViewModels
{
    public class InboxNewsViewModel
    {
        public List<News> NewsList { get; set; }

        public InboxNewsViewModel()
        {
            NewsList = new List<News>();
        }
    }
}
