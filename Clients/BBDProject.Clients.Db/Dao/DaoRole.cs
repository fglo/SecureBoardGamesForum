using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BBDProject.Clients.Db.Migrations;
using Microsoft.AspNetCore.Identity;

namespace BBDProject.Clients.Db.Dao
{
    /// <summary>
    /// role Table Data Access Object 
    /// </summary>
    [Table(DatabaseNames.RoleTableName, Schema = DatabaseNames.UsersSchemaName)]
    public class DaoRole : IdentityRole<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
    }
}
