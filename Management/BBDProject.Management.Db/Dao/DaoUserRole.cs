using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BBDProject.Management.Db.Dao
{
    /// <summary>
    /// AspNetUsers Table Data Access Object 
    /// </summary>
    [Table("user_role", Schema = "users")]
    public class DaoUserRole : IdentityUserRole<int>
    {
    }
}
