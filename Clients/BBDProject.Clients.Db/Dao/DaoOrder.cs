using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBDProject.Clients.Db.Dao
{
    [Table("order")]

    public class DaoOrder
    {
        [Column("id")] public int Id { get; set; }
        [Column("id_user")] public int UserId { get; set; }
        [ForeignKey("UserId")] public DaoUser User { get; set; }
        [Column("invoice")] public bool Invoice { get; set; }
        [Column("date_added")] public DateTime DateAdded { get; set; }
    }
}
