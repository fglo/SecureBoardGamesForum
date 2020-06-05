using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBDProject.Clients.Db.Dao
{
    [Table("product_opinion")]

    public class DaoProductOpinion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        [Column("id")] public int Id { get; set; }
        [Column("id_product")] public int ProductId { get; set; }
        [ForeignKey("ProductId")] public DaoProduct Product { get; set; }
        [Column("id_user")] public int UserId { get; set; }
        [ForeignKey("UserId")] public DaoUser User { get; set; }
        [Column("content")] public string Content { get; set; }
        [Column("stars")] public int Stars { get; set; }
        [Column("date_added")] public DateTime DateAdded { get; set; }
        [Column("date_modified")] public DateTime DateModified { get; set; }
    }
}
