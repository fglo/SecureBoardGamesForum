using System;
using BBDProject.Shared.Models.User;

namespace BBDProject.Clients.Models.Chat
{
    public class MessageModel
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public UserModel Author { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMyMessage { get; set; }
    }
}
