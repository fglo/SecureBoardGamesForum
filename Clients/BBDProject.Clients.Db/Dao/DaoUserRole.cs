using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BBDProject.Clients.Db.Migrations;
using Microsoft.AspNetCore.Identity;

namespace BBDProject.Clients.Db.Dao
{
    /// <summary>
    /// user Table Data Access Object 
    /// </summary>
    [Table(DatabaseNames.UserRoleTableName, Schema = DatabaseNames.UsersSchemaName)]
    public class DaoUserRole : IdentityUserRole<int>
    {
    }
}
