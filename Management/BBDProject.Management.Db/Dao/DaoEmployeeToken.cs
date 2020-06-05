using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BBDProject.Management.Db.Dao
{
    /// <summary>
    /// user_token Table Data Access Object 
    /// </summary>
    [Table(DatabaseNames.UserTokenTableName, Schema = DatabaseNames.UsersSchemaName)]
    public class DaoEmployeeToken : IdentityUserToken<int>
    {
    }
}
