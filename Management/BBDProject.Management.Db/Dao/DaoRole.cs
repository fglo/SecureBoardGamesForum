using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BBDProject.Management.Db.Dao
{
    /// <summary>
    /// AspNetRoles Table Data Access Object 
    /// </summary>
    [Table("role", Schema = "users")]
    public class DaoRole : IdentityRole<int>
    {
    }
}
