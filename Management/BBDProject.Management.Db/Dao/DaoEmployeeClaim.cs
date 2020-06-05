using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BBDProject.Management.Db.Dao
{
    /// <summary>
    /// user Table Data Access Object 
    /// </summary>
    [Table(DatabaseNames.UserClaimTableName, Schema = DatabaseNames.UsersSchemaName)]
    public class DaoEmployeeClaim : IdentityUserClaim<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
    }
}
