using crowdfunding_application.Models;
using crowdfunding_application.Models.Services;
using crowdfunding_application.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace crowdfunding_application.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ICampaignService _campaignService;
        private readonly INewsService _newsService;
        private readonly UserManager<IdentityUser> _userManager;

        public TransactionController(ITransactionService transactionService, ICampaignService campaignService, UserManager<IdentityUser> userManager, INewsService newsService)
        {
            _transactionService = transactionService;
            _campaignService = campaignService;
            _userManager = userManager;
            _newsService = newsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Pay(int? id)
        {
            return View(new PaymentTransaction() { CampaignId = (int)id });
        }

        [HttpPost]
        public async Task<IActionResult> Pay(PaymentTransaction transaction)
        {
            var campaign = await _campaignService.Get(transaction.CampaignId);
            var transactionNew = new PaymentTransaction()
            {
                CampaignId = campaign.Id,
                UserId = _userManager.GetUserId(User),
                Amount = transaction.Amount
            };
            await _transactionService.Create(transactionNew);
            campaign.MoneyActual += transactionNew.Amount;
            if(campaign.MoneyActual >= campaign.MoneyGoal)
            {
                campaign.IsFinished = true;
                News news = new()
                {
                    CampaignId = campaign.Id,
                    Title = $"{campaign.Title} - campaign finished!",
                    Description = $"Campaign goal reached in " +
                    $"{(int)(DateTime.Now - campaign.CreationDate).TotalDays} days, " +
                    $"{(int)(DateTime.Now - campaign.CreationDate).TotalHours} hours, " +
                    $"{(int)(DateTime.Now - campaign.CreationDate).TotalHours} minutes",
                    CreationDate = DateTime.Now,
                    Image = campaign.HomeImage
                };
                await _newsService.Create(news);
            }
            await _campaignService.Update(campaign);
            // redirect to payments statistics
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Transactions()
        {
            var transactionsViewModel = new TransactionsViewModel();
            transactionsViewModel.Transactions.AddRange(await _transactionService.GetJoin(item => item.UserId == _userManager.GetUserId(User)));
            return View(transactionsViewModel);
        }
    }
}
