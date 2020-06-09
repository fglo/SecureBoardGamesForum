using BBDProject.Shared.Models.User;
using System;
using System.Collections.Generic;

namespace BBDProject.Clients.Models.Forum
{
    public class ForumTopicViewModel
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public UserModel Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime DateAdded { get; set; }
        public string DateModifiedStr
        {
            get
            {
                if (DateModified == null || DateModified.Equals(DateTime.MinValue))
                    return DateAdded.ToString("dd.MM.yyyy hh:mm");
                else
                    return DateModified?.ToString("dd.MM.yyyy hh:mm");
            }
        }

        public List<ForumPostViewModel> ForumPosts { get; set; }
    }
}
