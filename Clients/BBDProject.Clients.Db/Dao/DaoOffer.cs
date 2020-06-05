using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBDProject.Clients.Db.Dao
{
    [Table("offer")]

    public class DaoOffer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        [Column("id")] public int Id { get; set; }
        [Column("id_user")] public int UserId { get; set; }
        [ForeignKey("UserId")] public DaoUser User { get; set; }
        [Column("id_product")] public int ProductId { get; set; }
        [ForeignKey("ProductId")] public DaoProduct Product { get; set; }
        [Column("name")] public string Name { get; set; }
        [Column("brand")] public string Brand { get; set; }
        [Column("model")] public string Model { get; set; }
        [Column("date_added")] public DateTime DateAdded { get; set; }
    }
}
