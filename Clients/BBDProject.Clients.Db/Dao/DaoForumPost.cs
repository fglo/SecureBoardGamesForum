using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBDProject.Clients.Db.Dao
{
    [Table("forum_post")]
    public class DaoForumPost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] public int Id { get; set; }
        [Column("id_author")] public int AuthorId { get; set; }
        [ForeignKey("AuthorId")] public DaoUser Author { get; set; }
        [Column("id_forum_topic")] public int ForumTopicId { get; set; }
        [ForeignKey("ForumTopicId")] public DaoForumTopic ForumTopic { get; set; }
        [Column("content")] public string Content { get; set; }
        [Column("date_modified")] public DateTime? DateModified { get; set; }
        [Column("date_added")] public DateTime DateAdded { get; set; }
        [Column("deleted")] public bool Deleted { get; set; }
    }
}
