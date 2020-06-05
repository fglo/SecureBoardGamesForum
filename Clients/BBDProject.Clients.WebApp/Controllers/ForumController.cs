using System.Net;
using System.Threading.Tasks;
using BBDProject.Clients.Db.Dao;
using BBDProject.Clients.Services.Chat;
using BBDProject.Clients.Services.Forum;
using BBDProject.Clients.Services.User;
using BBDProject.Shared.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BBDProject.Clients.WebApp.Controllers
{
    public class ForumController : BaseController
    {
        private readonly IForumService _forumService;

        public ForumController(IForumService forumService)
        {
            _forumService = forumService;
        }

        [HttpGet]
        public async Task<IActionResult> MainPage()
        {
            return View(await _forumService.GetTopicPreviews());
        }

        [HttpGet("/Forum/Topic/{topicId}")]
        public async Task<IActionResult> Topic(int topicId)
        {
            return View(await _forumService.GetTopic(topicId));
        }
    }
}