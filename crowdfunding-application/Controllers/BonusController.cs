using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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
    [Authorize(Roles = "Administrator, User")]
    public class BonusController : Controller
    {
        private readonly IBonusService _bonusService;
        private readonly ITransactionService _transactionService;
        private readonly ICampaignService _campaignService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICloudinaryService _cloudinaryService;

        public BonusController(IBonusService bonusService, ITransactionService transactionService, ICampaignService campaignService, UserManager<IdentityUser> userManager, ICloudinaryService cloudinaryService)
        {
            _bonusService = bonusService;
            _transactionService = transactionService;
            _campaignService = campaignService;
            _userManager = userManager;
            _cloudinaryService = cloudinaryService;
        }
        [Authorize]
        public IActionResult Create(int? id)
        {
            return View(new CreateBonusViewModel() { CampaignId = (int)id});
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateBonusViewModel bonusViewModel)
        {
            bonusViewModel.Bonus.CampaignId = bonusViewModel.CampaignId;
            bonusViewModel.Bonus.Id = 0;

            var uploadResult = _cloudinaryService.UploadImage(bonusViewModel.MainImage);
            bonusViewModel.Bonus.Image = uploadResult.SecureUrl.AbsoluteUri;

            await _bonusService.Create(bonusViewModel.Bonus);

            return RedirectToAction("Index", "Home", bonusViewModel.CampaignId);
        }
    }
}
