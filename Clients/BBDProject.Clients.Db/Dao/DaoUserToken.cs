using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BBDProject.Clients.Db.Migrations;
using Microsoft.AspNetCore.Identity;

namespace BBDProject.Clients.Db.Dao
{
    /// <summary>
    /// user_token Table Data Access Object 
    /// </summary>
    [Table(DatabaseNames.UserTokenTableName, Schema = DatabaseNames.UsersSchemaName)]
    public class DaoUserToken : IdentityUserToken<int>
    {
    }
}
