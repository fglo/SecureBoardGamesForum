using System;
using BBDProject.Management.Db.Dao;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BBDProject.Management.Db
{
    public class ManagementDbContext : IdentityDbContext<DaoEmployee, DaoRole, int, DaoEmployeeClaim, DaoEmployeeRole, DaoEmployeeLogin, DaoRoleClaim, DaoEmployeeToken>
    {
        private static IConfigurationRoot _config;

        public ManagementDbContext(DbContextOptions<ManagementDbContext> options, IConfigurationRoot config) : base(options)
        {
            _config = config;
        }

        public ManagementDbContext(DbContextOptions<ManagementDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_config.GetConnectionString("ManagementConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DaoEmployee>()
                .HasBaseType((Type)null)
                .ToTable(DatabaseNames.UserTableName, DatabaseNames.UsersSchemaName)
                .HasKey(u => new { u.Id });
            modelBuilder.Entity<DaoEmployeeClaim>().ToTable(DatabaseNames.UserClaimTableName, DatabaseNames.UsersSchemaName)
                .HasBaseType((Type)null)
                .HasKey(uc => new { uc.Id });
            modelBuilder.Entity<DaoEmployeeLogin>().ToTable(DatabaseNames.UserLoginTableName, DatabaseNames.UsersSchemaName)
                .HasBaseType((Type)null)
                .HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
            modelBuilder.Entity<DaoEmployeeToken>().ToTable(DatabaseNames.UserTokenTableName, DatabaseNames.UsersSchemaName)
                .HasBaseType((Type)null)
                .HasKey(ut => new { ut.UserId });
            modelBuilder.Entity<DaoRole>().ToTable(DatabaseNames.RoleTableName, DatabaseNames.UsersSchemaName)
                .HasBaseType((Type)null)
                .HasKey(r => new { r.Id });
            modelBuilder.Entity<DaoRoleClaim>().ToTable(DatabaseNames.RoleClaimTableName, DatabaseNames.UsersSchemaName)
                .HasBaseType((Type)null)
                .HasKey(rc => new { rc.Id });
            modelBuilder.Entity<DaoEmployeeRole>().ToTable(DatabaseNames.UserRoleTableName, DatabaseNames.UsersSchemaName)
                .HasBaseType((Type)null)
                .HasKey(ur => new { ur.UserId, ur.RoleId });
        }

        //public virtual DbSet<DaoTest> Tests { get; set; }
    }
}
