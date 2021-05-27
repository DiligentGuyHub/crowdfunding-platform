using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crowdfunding_application.Models.Services;
using crowdfunding_application.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace crowdfunding_application.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministrationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICampaignService _campaignService;
        private readonly INewsService _newsService;
        private readonly IBonusService _bonusService;
        private readonly ICommentService _commentService;
        private readonly IRatingService _ratingService;

        public AdministrationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ICampaignService campaignService, INewsService newsService, IBonusService bonusService, ICommentService commentService, IRatingService ratingService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _campaignService = campaignService;
            _newsService = newsService;
            _bonusService = bonusService;
            _commentService = commentService;
            _ratingService = ratingService;
        }

        public async Task<IActionResult> Display()
        {
            var detailsViewModel = new DisplayAdministrationViewModel();
            detailsViewModel.Users.AddRange(await _userManager.GetUsersInRoleAsync("User"));
            detailsViewModel.Administrators.AddRange(await _userManager.GetUsersInRoleAsync("Administrator"));
            detailsViewModel.Blocked.AddRange(await _userManager.GetUsersInRoleAsync("Blocked"));
            return View(detailsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(IdentityUser user)
        {
            var result = await _userManager.FindByEmailAsync(user.Id);
            if (result != null)
            {
                await _userManager.DeleteAsync(result);
                var campaigns = await _campaignService.GetJoin(item => item.UserId == result.Id);
                foreach (var item in campaigns)
                {
                    await _campaignService.Delete(item.Id);

                }
                return RedirectToAction("Display", "Administration");
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> ChangeRole(IdentityUser user, string rl)
        {
            var result = await _userManager.FindByEmailAsync(user.Id);
            if (result != null)
            {
                var role = await _userManager.GetRolesAsync(result);
                string rolename = role.FirstOrDefault();
                await _userManager.RemoveFromRoleAsync(result, rolename);
                await _userManager.AddToRoleAsync(result, rl);
                return RedirectToAction("Display", "Administration");
            }
            return NotFound();
        }
    }
}
