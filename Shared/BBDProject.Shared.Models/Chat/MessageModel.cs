using System;
using System.ComponentModel.DataAnnotations.Schema;
using BBDProject.Shared.Models.User;

namespace BBDProject.Shared.Models.Chat
{
    public class MessageModel
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public UserModel Author { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
