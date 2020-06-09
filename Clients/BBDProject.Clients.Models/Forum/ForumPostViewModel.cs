using BBDProject.Shared.Models.User;
using System;

namespace BBDProject.Clients.Models.Forum
{
    public class ForumPostViewModel
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public UserModel Author { get; set; }
        public int ForumTopicId { get; set; }
        public ForumTopicViewModel ForumTopic { get; set; }
        public string Content { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
