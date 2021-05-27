using crowdfunding_application.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.Hubs
{
    public class CommentHub : Hub
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<IdentityUser> _userManager;
        public CommentHub(ICommentService commentService, UserManager<IdentityUser> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
        }
        [Authorize]
        public async Task SendComment(int campaignId, string message)
        {
            await _commentService.Create(new Comment
            {
                CampaignId = campaignId,
                UserName = Context.User.Identity.Name,
                Content = message
            });
            await Clients.All.SendAsync("RecieveComment", Context.User.Identity.Name, message);
        }
    }
}
