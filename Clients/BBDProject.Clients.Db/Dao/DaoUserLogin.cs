using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BBDProject.Clients.Db.Migrations;
using Microsoft.AspNetCore.Identity;

namespace BBDProject.Clients.Db.Dao
{
    /// <summary>
    /// user Table Data Access Object 
    /// </summary>
    [Table(DatabaseNames.UserLoginTableName, Schema = DatabaseNames.UsersSchemaName)]
    public class DaoUserLogin : IdentityUserLogin<int>
    {
    }
}
