using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBDProject.Management.Db.Dao
{
    [Table("activity")]
    public class DaoActivity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("table_name")]
        public string TableName { get; set; }
        [Column("activity_name")]
        public string ActivityName { get; set; }
    }
}
