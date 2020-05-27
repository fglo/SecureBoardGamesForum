using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBDProject.Clients.Db.Dao
{
    [Table("product")]

    public class DaoProduct
    {
        [Column("id")] public int Id { get; set; }
        [Column("name")] public string Name { get; set; }
        [Column("brand")] public string Brand { get; set; }
        [Column("model")] public string Model { get; set; }
        [Column("date_added")] public DateTime DateAdded { get; set; }
    }
}
