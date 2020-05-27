using System;
using BBDProject.Management.Db.Dao;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BBDProject.Management.Db
{
    public class ManagementDbContext : IdentityDbContext<DaoEmployee, DaoRole, int, DaoUserClaim, DaoUserRole, DaoUserLogin, DaoRoleClaim, DaoUserToken>
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
                .ToTable("user", "users")
                .HasKey(l => new { l.Id });
            modelBuilder.Entity<DaoUserClaim>().ToTable("user_claim", "users")
                .HasBaseType((Type)null)
                .HasKey(l => new { l.Id });
            modelBuilder.Entity<DaoUserLogin>().ToTable("user_login", "users")
                .HasBaseType((Type)null)
                .HasKey(l => new { l.LoginProvider, l.ProviderKey });
            modelBuilder.Entity<DaoUserToken>().ToTable("user_token", "users")
                .HasBaseType((Type)null)
                .HasKey(l => new { l.UserId });
            modelBuilder.Entity<DaoRole>().ToTable("role", "users")
                .HasBaseType((Type)null)
                .HasKey(l => new { l.Id });
            modelBuilder.Entity<DaoRoleClaim>().ToTable("role_claim", "users")
                .HasBaseType((Type)null)
                .HasKey(l => new { l.Id });
            modelBuilder.Entity<DaoUserRole>().ToTable("user_role", "users")
                .HasBaseType((Type)null)
                .HasKey(r => new { r.UserId, r.RoleId });
        }

        //public virtual DbSet<DaoTest> Tests { get; set; }
    }
}
