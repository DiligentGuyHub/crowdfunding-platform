using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using crowdfunding_application.Data;
using crowdfunding_application.Models;
using crowdfunding_application.Models.CloudinaryService;
using crowdfunding_application.Models.Services;
using crowdfunding_application.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Controllers
{
    public class CampaignController : Controller
    {
        private readonly ICampaignService _campaignService;
        private readonly IBonusService _bonusService;
        private readonly INewsService _newsService;
        private readonly IRatingService _ratingService;
        private readonly ICommentService _commentService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly UserManager<IdentityUser> _userManager;
        private DetailsCampaignViewModel _detailsCampaignViewModel;


        public CampaignController(ICampaignService campaignService, UserManager<IdentityUser> userManager, INewsService newsService, IBonusService bonusService, ICloudinaryService cloudinaryService, IRatingService ratingService, ICommentService commentService)
        {
            _campaignService = campaignService;
            _userManager = userManager;
            _newsService = newsService;
            _bonusService = bonusService;
            _cloudinaryService = cloudinaryService;
            _detailsCampaignViewModel = new DetailsCampaignViewModel();
            _ratingService = ratingService;
            _commentService = commentService;
        }

        [Authorize]
        //public IActionResult Create()
        //{
        //    var campaign = new Campaign() { Title = "New campaign" };
        //    var viewmodel = new CreateCampaignViewModel()
        //    {
        //        Campaign = campaign
        //    };
        //    return View(viewmodel);
        //}
        [Authorize]
        public async Task<IActionResult> Inbox()
        {
            var _inboxCampaignViewModel = new InboxCampaignViewModel();
            _inboxCampaignViewModel.Campaigns.AddRange(await _campaignService.GetJoin(item => item.UserId == _userManager.GetUserId(User)));
            return View(_inboxCampaignViewModel);
        
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCampaignViewModel campaignViewModel)
        {
            campaignViewModel.Campaign.UserId = _userManager.GetUserId(User);
            campaignViewModel.Campaign.CreationDate = DateTime.Now.ToLocalTime().ToLocalTime();
            campaignViewModel.Campaign.IsFinished = false;

            var uploadResult = _cloudinaryService.UploadImage(campaignViewModel.MainImage);

            campaignViewModel.Campaign.HomeImage = uploadResult.SecureUrl.AbsoluteUri;

            var campaign = await _campaignService.Create(campaignViewModel.Campaign);
            News news = new()
            {
                CampaignId = campaign.Id,
                Title = $"{ campaignViewModel.Campaign.Title} - brand new campaign by {_userManager.GetUserName(User)}!",
                Description = campaignViewModel.Campaign.Description,
                CreationDate = DateTime.Now.ToLocalTime(),
                Image = uploadResult.SecureUrl.AbsoluteUri
            };

            await _newsService.Create(news);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Campaign campaign = await _campaignService.Get(id);
                if (campaign != null)
                {
                    var editCampaignViewModel = new EditCampaignViewModel
                    {
                        Campaign = campaign
                    };
                    return View(editCampaignViewModel);

                }
            }
            return NotFound();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditCampaignViewModel editCampaignViewModel)
        {
            if (editCampaignViewModel.MainImage != null)
            {
                var uploadResult = _cloudinaryService.UploadImage(editCampaignViewModel.MainImage);

                editCampaignViewModel.Campaign.HomeImage = uploadResult.SecureUrl.AbsoluteUri;
            }

            await _campaignService.Update(editCampaignViewModel.Campaign);
            News news = new News()
            {
                CampaignId = editCampaignViewModel.Campaign.Id,
                Title = $"{ editCampaignViewModel.Campaign.Title} - see updates on campaign",
                Description = string.Empty,
                CreationDate = DateTime.Now.ToLocalTime(),
                Image = editCampaignViewModel.Campaign.HomeImage
            };

            await _newsService.Create(news);
            return RedirectToAction("Inbox");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Campaign campaign = await _campaignService.Get(id);
                if (campaign != null)
                    return View(campaign);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Campaign campaign = await _campaignService.Get(id);
                if (campaign != null)
                {
                    // Delete Campaign
                    await _campaignService.Delete(id);
                    // Delete all news about deleted campaign
                    foreach (var item in await _newsService.GetJoin(item => item.CampaignId == id))
                    {
                        await _newsService.Delete(item.Id);
                    }
                    return RedirectToAction("Inbox");
                }
            }
            return NotFound();
        }

        [HttpGet]
        [ActionName("Finish")]
        public async Task<IActionResult> FinishCampaign(int? id)
        {
            if (id != null)
            {
                Campaign campaign = await _campaignService.Get(id);
                if (campaign != null)
                    return View(campaign);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Finish(int? id)
        {
            if (id != null)
            {
                Campaign campaign = await _campaignService.Get(id);
                if (campaign != null)
                {
                    campaign.IsFinished = true;
                    await _campaignService.Update(campaign.Id, campaign);
                    News news = new()
                    {
                        CampaignId = campaign.Id,
                        Title = $"{campaign.Title} - campaign finished!",
                        Description = $"Campaign goal reached in " +
                        $"{(int)(DateTime.Now.ToLocalTime() - campaign.CreationDate).TotalDays} days, " +
                        $"{(int)(DateTime.Now.ToLocalTime() - campaign.CreationDate).TotalHours} hours, " +
                        $"{(int)(DateTime.Now.ToLocalTime() - campaign.CreationDate).TotalHours} minutes",
                        CreationDate = DateTime.Now.ToLocalTime(),
                        Image = campaign.HomeImage
                    };
                    await _newsService.Create(news);

                    return RedirectToAction("Details", "Campaign", new { id });
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> Details(int? id)
        {
            Campaign campaign = await _campaignService.Get(id);
            _detailsCampaignViewModel = new DetailsCampaignViewModel()
            {
                campaign = campaign,
                NewsList = new List<News>(await _newsService.GetJoin(item => item.CampaignId == campaign.Id)),
                BonusList = new List<Bonus>(await _bonusService.GetJoin(item => item.CampaignId == campaign.Id)),
                CommentHistory = new List<Comment>(await _commentService.GetJoin(item => item.CampaignId == campaign.Id))
            };

            ViewBag.CurrentUserId = _userManager.GetUserId(User);
            ViewBag.CreatorId = campaign.UserId;

            var allRatings = await _ratingService.GetJoin(item => item.CampaignId == id);
            if (allRatings.Count() != 0)
            {
                int scores = allRatings.Sum(item => item.Score);
                double average = (double)scores / (double)allRatings.Count();
                _detailsCampaignViewModel.AverageRating = average;
            }
            var MyRating = allRatings.Where(item => item.UserId == _userManager.GetUserId(User)).FirstOrDefault();
            if (MyRating != null)
                ViewBag.MyRating = MyRating.Score;
            else
                ViewBag.MyRating = 0;
            return View(_detailsCampaignViewModel);
        }
        public async Task<IActionResult> Rate(int? id, int? rating)
        {
            var allRatings = await _ratingService.GetJoin(item => item.CampaignId == id);
            var campaign = await _campaignService.Get(id);
            if (campaign.UserId == _userManager.GetUserId(User))
            {
                return RedirectToAction("Details", "Campaign", new { id });
            }

            if (allRatings.Count() == 0)
            {
                await _ratingService.Create(new Rating
                {
                    UserId = _userManager.GetUserId(User),
                    CampaignId = (int)id,
                    Score = (int)rating
                });
            }
            else
            {
                var previousRating = allRatings.Where(item => item.UserId == _userManager.GetUserId(User)).FirstOrDefault();
                if(previousRating == null)
                {
                    await _ratingService.Create(new Rating
                    {
                        UserId = _userManager.GetUserId(User),
                        CampaignId = (int)id,
                        Score = (int)rating
                    });
                }
                else
                {
                    previousRating.Score = (int)rating;
                    await _ratingService.Update(previousRating.Id, previousRating);
                }
            }
            return RedirectToAction("Details", "Campaign", new { id });
        }

    }
}
