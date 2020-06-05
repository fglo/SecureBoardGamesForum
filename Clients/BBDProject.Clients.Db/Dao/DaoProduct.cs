using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBDProject.Clients.Db.Dao
{
    [Table("product")]

    public class DaoProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] public int Id { get; set; }
        [Column("name")] public string Name { get; set; }
        [Column("name_normalized")] public string NameNormalized { get; set; }
        [Column("brand")] public string Brand { get; set; }
        [Column("model")] public string Model { get; set; }
        [Column("description")] public string Description { get; set; }
        [Column("date_added")] public DateTime DateAdded { get; set; }
        [Column("image")] public byte[] Image{ get; set; }
    }
}
