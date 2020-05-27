using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BBDProject.Management.Db.Dao
{
    /// <summary>
    /// AspNetUserTokens Table Data Access Object 
    /// </summary>
    [Table("AspNetUserTokens", Schema = "users")]
    public class DaoUserToken : IdentityUserToken<int>
    {
    }
}
