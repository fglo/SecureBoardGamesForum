﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BBDProject.Management.Db.Dao
{
    /// <summary>
    /// AspNetUsers Table Data Access Object 
    /// </summary>
    [Table("role_claim", Schema = "users")]
    public class DaoRoleClaim : IdentityRoleClaim<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
    }
}
