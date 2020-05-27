using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBDProject.Management.Db.Dao
{
    [Table("user_activity")]
    public class DaoUserActivity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] public int Id { get; set; }
        [Column("id_user")] public int UserId { get; set; }
        [ForeignKey("UserId")] public DaoEmployee Employee { get; set; }
        [Column("id_activity")] public int ActivityId { get; set; }
        [ForeignKey("ActivityId")] public DaoActivity Activity { get; set; }
        [Column("date_added")] public DateTime DateAdded { get; set; }
    }
}
