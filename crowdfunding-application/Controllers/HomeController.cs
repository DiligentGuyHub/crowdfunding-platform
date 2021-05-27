using crowdfunding_application.Models;
using crowdfunding_application.Models.Services;
using crowdfunding_application.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewsService _newsService;
        private readonly InboxNewsViewModel _inboxNewsViewModel;

        public HomeController(ILogger<HomeController> logger, INewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
            _inboxNewsViewModel = new InboxNewsViewModel();
        }
        [Authorize(Roles = "Administrator, User")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var news = new List<News>();
            news.AddRange(await _newsService.GetAllSorted());
            
            _inboxNewsViewModel.NewsList = news;
            return View(_inboxNewsViewModel);
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
