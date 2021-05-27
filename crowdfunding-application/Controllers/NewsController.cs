using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using crowdfunding_application.Models;
using crowdfunding_application.Models.CloudinaryService;
using crowdfunding_application.Models.Services;
using crowdfunding_application.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Controllers
{
    [Authorize(Roles = "Administrator, User")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ICloudinaryService _cloudinaryService;

        public NewsController(INewsService newsService, ICloudinaryService cloudinaryService)
        {
            _newsService = newsService;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                News news = await _newsService.Get(id);
                if (news != null)
                {
                    return View(news);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Index(News news)
        {
            await _newsService.Delete(news.Id);
            News newsUpdated = new()
            {
                CampaignId = news.CampaignId,
                Title = news.Title,
                Description = news.Description,
                Image = news.Image,
                CreationDate = DateTime.Now.ToLocalTime().ToLocalTime()
            };
            await _newsService.Create(newsUpdated);
            return RedirectToAction("Inbox", "Campaign");
        }

        [Authorize]
        public IActionResult Create(int? id)
        {
            return View(new CreateNewsViewModel() { CampaignId = (int)id});
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateNewsViewModel newsViewModel)
        {
            newsViewModel.News.CreationDate = DateTime.Now.ToLocalTime().ToLocalTime();
            newsViewModel.News.CampaignId = newsViewModel.CampaignId;

            var uploadResult = _cloudinaryService.UploadImage(newsViewModel.MainImage);

            newsViewModel.News.Image = uploadResult.SecureUrl.AbsoluteUri;
            await _newsService.Create(newsViewModel.News);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                News news = await _newsService.Get(id);
                if (news != null)
                    return View(news);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                News news = await _newsService.Get(id);
                if (news != null)
                {
                    await _newsService.Delete(id);
                    return RedirectToAction("Index", "Home");
                }
            }
            return NotFound();
        }

    }
}
