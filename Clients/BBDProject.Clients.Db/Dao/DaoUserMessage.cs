using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBDProject.Clients.Db.Dao
{
    [Table("user_message")]

    public class DaoUserMessage
    {
        [Column("id")] public int Id { get; set; }
        [Column("id_user")] public int UserId { get; set; }
        [ForeignKey("UserId")] public DaoUser User { get; set; }
        [Column("id_message")] public int MessageId { get; set; }
        [ForeignKey("MessageId")] public DaoMessage Message { get; set; }
    }
}
