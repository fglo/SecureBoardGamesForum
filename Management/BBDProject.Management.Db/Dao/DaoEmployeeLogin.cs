using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BBDProject.Management.Db.Dao
{
    /// <summary>
    /// user Table Data Access Object 
    /// </summary>
    [Table(DatabaseNames.UserLoginTableName, Schema = DatabaseNames.UsersSchemaName)]
    public class DaoEmployeeLogin : IdentityUserLogin<int>
    {
    }
}
