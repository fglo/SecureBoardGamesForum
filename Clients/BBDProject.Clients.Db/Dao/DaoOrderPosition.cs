using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBDProject.Clients.Db.Dao
{
    [Table("order_position")]

    public class DaoOrderPosition
    {
        [Column("id")] public int Id { get; set; }
        [Column("id_order")] public int OrderId { get; set; }
        [ForeignKey("OrderId")] public DaoOrder Order{ get; set; }
        [Column("id_offer")] public int OfferId { get; set; }
        [ForeignKey("OrderId")] public DaoOffer Offer { get; set; }
        [Column("number")] public int Number { get; set; }
        [Column("price")] public double Price { get; set; }
        [Column("date_added")] public DateTime DateAdded { get; set; }
        [Column("date_modified")] public DateTime DateModified { get; set; }
        [Column("deleted")] public bool Deleted { get; set; }
    }
}
