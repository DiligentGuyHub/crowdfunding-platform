using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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
    public class BonusController : Controller
    {
        private readonly IBonusService _bonusService;
        private readonly ITransactionService _transactionService;
        private readonly ICampaignService _campaignService;
        private readonly UserManager<IdentityUser> _userManager;

        public BonusController(IBonusService bonusService, ITransactionService transactionService, ICampaignService campaignService, UserManager<IdentityUser> userManager)
        {
            _bonusService = bonusService;
            _transactionService = transactionService;
            _campaignService = campaignService;
            _userManager = userManager;
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

            Account account = new Account(
                "dkmufkwfv",
                "215513762388953",
                "24h2YowdCkwIQMjMHnzijXp0aiE"
                );
            Cloudinary cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(@"C:\Users\longr\OneDrive\Изображения\" + bonusViewModel.MainImage.FileName)
            };
            var uploadResult = cloudinary.Upload(uploadParams);

            bonusViewModel.Bonus.Image = uploadResult.SecureUrl.AbsoluteUri;
            await _bonusService.Create(bonusViewModel.Bonus);

            return RedirectToAction("Index", "Home", bonusViewModel.CampaignId);
        }
    }
}
