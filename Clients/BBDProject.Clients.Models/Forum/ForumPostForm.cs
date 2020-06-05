namespace BBDProject.Clients.Models.Forum
{
    public class ForumPostForm
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int ForumTopicId { get; set; }
        public string Content { get; set; }
    }
}
