using crowdfunding_application.Models;
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
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ICampaignService _campaignService;
        private readonly IBonusService _bonusService;
        private readonly IBonusTransactionService _bonusTransactionService;
        private readonly INewsService _newsService;
        private readonly UserManager<IdentityUser> _userManager;

        public TransactionController(ITransactionService transactionService, ICampaignService campaignService, UserManager<IdentityUser> userManager, INewsService newsService, IBonusService bonusService, IBonusTransactionService bonusTransactionService)
        {
            _transactionService = transactionService;
            _campaignService = campaignService;
            _userManager = userManager;
            _newsService = newsService;
            _bonusService = bonusService;
            _bonusTransactionService = bonusTransactionService;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Pay(int? id)
        {
            return View(new PaymentTransaction() { CampaignId = (int)id });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Pay(PaymentTransaction transaction)
        {
            var campaign = await _campaignService.Get(transaction.CampaignId);

            var transactionNew = new PaymentTransaction()
            {
                CampaignId = campaign.Id,
                UserId = _userManager.GetUserId(User),
                Amount = transaction.Amount,
                Date = DateTime.Now.ToLocalTime().ToLocalTime()
            };
            await _transactionService.Create(transactionNew);

            campaign.MoneyActual += transactionNew.Amount;
            if (campaign.MoneyActual >= campaign.MoneyGoal)
            {
                campaign.IsFinished = true;
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
            }
            await _campaignService.Update(campaign);

            var bonusList = await _bonusService.GetJoin(item => item.CampaignId == campaign.Id);
            var bonuses = bonusList.Where(item => transaction.Amount >= item.Money).OrderBy(item=> item.Money);
            if(bonuses.Count() != 0)
            {
                var bonus = bonuses.Last();
                await _bonusTransactionService.Create(new BonusTransaction()
                {
                    UserId = _userManager.GetUserId(User),
                    BonusId = bonus.Id
                });
            }


            // redirect to payments statistics
            return RedirectToAction("Details", "Campaign", new { id = campaign.Id });
        }
        [Authorize]
        public async Task<IActionResult> Transactions()
        {
            var transactionsViewModel = new TransactionsViewModel();
            transactionsViewModel.Transactions.AddRange(await _transactionService.GetJoin(item => item.UserId == _userManager.GetUserId(User)));
            return View(transactionsViewModel);
        }
        [Authorize]
        public async Task<IActionResult> Bonuses()
        {
            var bonusesViewModel = new BonusesViewModel();
            bonusesViewModel.BonusTransactions.AddRange(await _bonusTransactionService.GetJoin(item => item.UserId == _userManager.GetUserId(User)));
            foreach(var transaction in bonusesViewModel.BonusTransactions)
            {
                var bonus = await _bonusService.Get(transaction.BonusId);
                bonusesViewModel.Bonuses.Add(bonus);
                var campaigns = await _campaignService.GetJoin(item => item.Id == bonus.CampaignId);
                bonusesViewModel.Campaigns.Add(campaigns.First());
            }
            return View(bonusesViewModel);
        }
    }
}
