using System.Threading.Tasks;
using BBDProject.Management.Services.Forum;
using Microsoft.AspNetCore.Mvc;

namespace BBDProject.Management.WebApp.Controllers
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

        [HttpGet]
        public async Task<IActionResult> DeleteTopic(int topicId)
        {
            await _forumService.DeleteTopic(topicId);
            return RedirectToAction("MainPage");
        }

        [HttpGet]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var topicId = await _forumService.DeletePost(postId);
            return RedirectToAction("Topic", new { topicId  = topicId });
        }
    }
}