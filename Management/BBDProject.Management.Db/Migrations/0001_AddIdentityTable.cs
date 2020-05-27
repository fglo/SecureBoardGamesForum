using FluentMigrator;

namespace BBDProject.Management.Db.Migrations
{
    [Migration(0001)]
    public class AddIdentityTable : Migration
    {
        private string _schemaName = "users";
        public override void Down()
        {
            Delete.Table("role").InSchema(_schemaName);
            Delete.Table("user_token").InSchema(_schemaName);
            Delete.Table("role_claim").InSchema(_schemaName);
            Delete.Table("user_claim").InSchema(_schemaName);
            Delete.Table("user_login").InSchema(_schemaName);
            Delete.Table("user_role").InSchema(_schemaName);
            Delete.Table("user").InSchema(_schemaName);
            Delete.Schema(_schemaName);
        }

        public override void Up()
        {
            Create.Schema(_schemaName);

            Create.Table("role").InSchema(_schemaName)
                .WithColumn("Id").AsInt32().PrimaryKey("PK_role").Identity()
                .WithColumn("ConcurrencyStamp").AsString().Nullable()
                .WithColumn("Name").AsString(256).NotNullable()
                .WithColumn("NormalizedName").AsString(256).Nullable()
                .Indexed("RoleNameIndex");

            Create.Table("user_token").InSchema(_schemaName)
                .WithColumn("UserId").AsInt32().PrimaryKey("PK_user_token").NotNullable()
                .WithColumn("LoginProvider").AsString(450).NotNullable()
                .WithColumn("Name").AsString(450).NotNullable()
                .WithColumn("Value").AsString().Nullable();

            Create.Table("user").InSchema(_schemaName)
                .WithColumn("Id").AsInt32().Identity().PrimaryKey("PK_user")
                .WithColumn("AccessFailedCount").AsInt32().NotNullable()
                .WithColumn("ConcurrencyStamp").AsString().Nullable()
                .WithColumn("Email").AsString(256).NotNullable()
                .WithColumn("EmailConfirmed").AsBoolean().NotNullable()
                .WithColumn("LockoutEnabled").AsBoolean().NotNullable()
                .WithColumn("LockoutEnd").AsCustom("timestamp with time zone").Nullable()
                .WithColumn("NormalizedEmail").AsString(256).Nullable()
                .WithColumn("NormalizedUserName").AsString(265).Nullable()
                .WithColumn("PasswordHash").AsString().Nullable()
                .WithColumn("PhoneNumber").AsString().Nullable()
                .WithColumn("PhoneNumberConfirmed").AsBoolean().NotNullable()
                .WithColumn("SecurityStamp").AsString().Nullable()
                .WithColumn("TwoFactorEnabled").AsBoolean().NotNullable()
                .WithColumn("UserName").AsString(256).NotNullable()
                .WithColumn("FirstName").AsString(256).Nullable()
                .WithColumn("LastName").AsString(256).Nullable();

            Create.Table("role_claim").InSchema(_schemaName)
                .WithColumn("Id").AsInt32().PrimaryKey("PK_role_claim").Identity()
                .WithColumn("ClaimType").AsString().Nullable()
                .WithColumn("ClaimValue").AsString().Nullable()
                .WithColumn("RoleId").AsInt32().NotNullable().Indexed("IX_role_claim_RoleId")
                    .ForeignKey("FK_role_claim_role_RoleId", _schemaName, "role", "Id");

            Create.Table("user_claim").InSchema(_schemaName)
                .WithColumn("Id").AsInt32().PrimaryKey("PK_user_claim").Identity()
                .WithColumn("ClaimType").AsString().Nullable()
                .WithColumn("ClaimValue").AsString().Nullable()
                .WithColumn("UserId").AsInt32().NotNullable().Indexed("IX_user_claim_UserId")
                    .ForeignKey("FK_user_claim_user_UserId", _schemaName, "user", "Id")
                .OnDelete(System.Data.Rule.Cascade);

            Create.Table("user_login").InSchema(_schemaName)
                .WithColumn("LoginProvider").AsInt32().NotNullable().PrimaryKey("PK_user_login")
                .WithColumn("ProviderKey").AsString(450).NotNullable().PrimaryKey("PK_user_login")
                .WithColumn("ProviderDisplayName").AsString().Nullable()
                .WithColumn("UserId").AsInt32()
                .NotNullable()
                .Indexed("IX_user_login_UserId")
                    .ForeignKey("FK_user_login_user_UserId", _schemaName, "user", "Id")
                .OnDelete(System.Data.Rule.Cascade);

            Create.Table("user_role").InSchema(_schemaName)
                .WithColumn("UserId").AsInt32().PrimaryKey("PK_user_role").Indexed("IX_user_role_UserId")
                    .ForeignKey("FK_user_role_user_UserId", _schemaName, "user", "Id")
                .WithColumn("RoleId").AsInt32().PrimaryKey("PK_user_role").Indexed("IX_user_role_RoleId")
                    .ForeignKey("FK_user_role_role_RoleId", _schemaName, "role", "Id")
                    .OnDelete(System.Data.Rule.Cascade);
        }
    }
}
