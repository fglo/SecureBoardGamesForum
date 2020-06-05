using BBDProject.Shared.Models.User;
using System;

namespace BBDProject.Clients.Models.Forum
{
    public class ForumTopicPreview
    {
        public int Id { get; set; }
        public string AuthorUserName { get; set; }
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
    }
}
