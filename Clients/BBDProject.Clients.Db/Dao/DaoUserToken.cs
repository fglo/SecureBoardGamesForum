using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BBDProject.Clients.Db.Dao
{
    /// <summary>
    /// user_token Table Data Access Object 
    /// </summary>
    [Table("user_token", Schema = "users")]
    public class DaoUserToken : IdentityUserToken<int>
    {
    }
}
