using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBDProject.Clients.Db.Dao
{
    [Table("message")]

    public class DaoMessage
    {
        [Column("id")] public int Id { get; set; }
        [Column("id_author")] public int AuthorId { get; set; }
        [ForeignKey("AuthorId")] public DaoUser Author { get; set; }
        [Column("content")] public string Content { get; set; }
        [Column("date_added")] public DateTime DateAdded { get; set; }
    }
}
