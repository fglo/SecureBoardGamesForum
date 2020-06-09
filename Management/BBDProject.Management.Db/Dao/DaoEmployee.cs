using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BBDProject.Management.Db.Dao
{
    /// <summary>
    /// user Table Data Access Object 
    /// </summary>
    [Table(DatabaseNames.UserTableName, Schema = DatabaseNames.UsersSchemaName)]
    public class DaoEmployee : IdentityUser<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped] public bool LockedOut => this.LockoutEnabled;
    }
}
