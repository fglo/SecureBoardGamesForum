using System;
using System.Collections.Generic;
using System.Text;
using BBDProject.Clients.Db.Dao;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BBDProject.Clients.Db
{
    public class ClientDbContext : IdentityDbContext<DaoUser, DaoRole, int, DaoUserClaim, DaoUserRole, DaoUserLogin, DaoRoleClaim, DaoUserToken>
    {
        private static IConfigurationRoot _config;

        public ClientDbContext(DbContextOptions<ClientDbContext> options, IConfigurationRoot config) : base(options)
        {
            _config = config;
        }

        public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_config.GetConnectionString("ClientConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DaoUser>()
                .HasBaseType((Type)null)
                .ToTable("user", "users")
                .HasKey(u => new { u.Id });
            modelBuilder.Entity<DaoUserClaim>().ToTable("user_claim", "users")
                .HasBaseType((Type)null)
                .HasKey(uc => new { uc.Id });
            modelBuilder.Entity<DaoUserLogin>().ToTable("user_login", "users")
                .HasBaseType((Type)null)
                .HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
            modelBuilder.Entity<DaoUserToken>().ToTable("user_token", "users")
                .HasBaseType((Type)null)
                .HasKey(ut => new { ut.UserId });
            modelBuilder.Entity<DaoRole>().ToTable("role", "users")
                .HasBaseType((Type)null)
                .HasKey(r => new { r.Id });
            modelBuilder.Entity<DaoRoleClaim>().ToTable("role_claim", "users")
                .HasBaseType((Type)null)
                .HasKey(rc => new { rc.Id });
            modelBuilder.Entity<DaoUserRole>().ToTable("user_role", "users")
                .HasBaseType((Type)null)
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<DaoProduct>().ToTable("products");
            modelBuilder.Entity<DaoOffer>().ToTable("offer");
            modelBuilder.Entity<DaoOrder>().ToTable("order");
            modelBuilder.Entity<DaoOrderPosition>().ToTable("order_position");
            modelBuilder.Entity<DaoProductOpinion>().ToTable("product_opinion");
            modelBuilder.Entity<DaoMessage>().ToTable("message");
            modelBuilder.Entity<DaoUserMessage>().ToTable("user_message");
        }

        public DbSet<DaoProduct> Products { get; set; }
        public DbSet<DaoOffer> Offers { get; set; }
        public DbSet<DaoOrder> Orders { get; set; }
        public DbSet<DaoOrderPosition> OrderPositions { get; set; }
        public DbSet<DaoProductOpinion> ProductOpinions { get; set; }
        public DbSet<DaoMessage> Messages { get; set; }
        public DbSet<DaoUserMessage> UserMessages { get; set; }
    }
}
