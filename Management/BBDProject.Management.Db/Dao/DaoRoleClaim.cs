using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BBDProject.Management.Db.Dao
{
    /// <summary>
    /// user Table Data Access Object 
    /// </summary>
    [Table(DatabaseNames.RoleClaimTableName, Schema = DatabaseNames.UsersSchemaName)]
    public class DaoRoleClaim : IdentityRoleClaim<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
    }
}
