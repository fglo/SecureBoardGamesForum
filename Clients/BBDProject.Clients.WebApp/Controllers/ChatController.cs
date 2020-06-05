using System.Net;
using System.Threading.Tasks;
using BBDProject.Clients.Db.Dao;
using BBDProject.Clients.Services.Chat;
using BBDProject.Clients.Services.User;
using BBDProject.Shared.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BBDProject.Clients.WebApp.Controllers
{
    public class ChatController : BaseController
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody]string message)
        {
            await _chatService.SendMessage(message);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetLastMessages()
        {
            var messages = await _chatService.GetLast();
            return Ok(messages);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var messages = await _chatService.GetPaged(10, 1);
            return Ok(messages);
        }

        [HttpGet]
        public async Task<IActionResult> GetPreviousPage()
        {
            var messages = await _chatService.GetPreviousPage(10);
            return Ok(messages);
        }
    }
}