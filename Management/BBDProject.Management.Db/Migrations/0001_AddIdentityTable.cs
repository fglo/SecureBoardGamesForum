using FluentMigrator;

namespace BBDProject.Management.Db.Migrations
{
    [Migration(0001)]
    public class AddIdentityTable : Migration
    {
        public override void Down()
        {
            Delete.Table(DatabaseNames.RoleTableName).InSchema(DatabaseNames.UsersSchemaName);
            Delete.Table(DatabaseNames.UserTokenTableName).InSchema(DatabaseNames.UsersSchemaName);
            Delete.Table(DatabaseNames.RoleClaimTableName).InSchema(DatabaseNames.UsersSchemaName);
            Delete.Table(DatabaseNames.UserClaimTableName).InSchema(DatabaseNames.UsersSchemaName);
            Delete.Table(DatabaseNames.UserLoginTableName).InSchema(DatabaseNames.UsersSchemaName);
            Delete.Table(DatabaseNames.RoleTableName).InSchema(DatabaseNames.UsersSchemaName);
            Delete.Table(DatabaseNames.UserTableName).InSchema(DatabaseNames.UsersSchemaName);
            Delete.Schema(DatabaseNames.UsersSchemaName);
        }

        public override void Up()
        {
            Create.Schema(DatabaseNames.UsersSchemaName);

            Create.Table(DatabaseNames.RoleTableName).InSchema(DatabaseNames.UsersSchemaName)
                .WithColumn("Id").AsInt32().PrimaryKey("PK_role").Identity()
                .WithColumn("ConcurrencyStamp").AsString().Nullable()
                .WithColumn("Name").AsString(256).NotNullable()
                .WithColumn("NormalizedName").AsString(256).Nullable()
                .Indexed("RoleNameIndex");

            Create.Table(DatabaseNames.UserTokenTableName).InSchema(DatabaseNames.UsersSchemaName)
                .WithColumn("UserId").AsInt32().PrimaryKey("PK_user_token").NotNullable()
                .WithColumn("LoginProvider").AsString(450).NotNullable()
                .WithColumn("Name").AsString(450).NotNullable()
                .WithColumn("Value").AsString().Nullable();

            Create.Table(DatabaseNames.UserTableName).InSchema(DatabaseNames.UsersSchemaName)
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

            Create.Table(DatabaseNames.RoleClaimTableName).InSchema(DatabaseNames.UsersSchemaName)
                .WithColumn("Id").AsInt32().PrimaryKey("PK_role_claim").Identity()
                .WithColumn("ClaimType").AsString().Nullable()
                .WithColumn("ClaimValue").AsString().Nullable()
                .WithColumn("RoleId").AsInt32().NotNullable().Indexed("IX_role_claim_RoleId")
                    .ForeignKey("FK_role_claim_role_RoleId", DatabaseNames.UsersSchemaName, DatabaseNames.RoleTableName, "Id");

            Create.Table(DatabaseNames.UserClaimTableName).InSchema(DatabaseNames.UsersSchemaName)
                .WithColumn("Id").AsInt32().PrimaryKey("PK_user_claim").Identity()
                .WithColumn("ClaimType").AsString().Nullable()
                .WithColumn("ClaimValue").AsString().Nullable()
                .WithColumn("UserId").AsInt32().NotNullable().Indexed("IX_user_claim_UserId")
                    .ForeignKey("FK_user_claim_user_UserId", DatabaseNames.UsersSchemaName, DatabaseNames.UserTableName, "Id")
                .OnDelete(System.Data.Rule.Cascade);

            Create.Table(DatabaseNames.UserLoginTableName).InSchema(DatabaseNames.UsersSchemaName)
                .WithColumn("LoginProvider").AsInt32().NotNullable().PrimaryKey("PK_user_login")
                .WithColumn("ProviderKey").AsString(450).NotNullable().PrimaryKey("PK_user_login")
                .WithColumn("ProviderDisplayName").AsString().Nullable()
                .WithColumn("UserId").AsInt32()
                .NotNullable()
                .Indexed("IX_user_login_UserId")
                    .ForeignKey("FK_user_login_user_UserId", DatabaseNames.UsersSchemaName, DatabaseNames.UserTableName, "Id")
                .OnDelete(System.Data.Rule.Cascade);

            Create.Table(DatabaseNames.UserRoleTableName).InSchema(DatabaseNames.UsersSchemaName)
                .WithColumn("UserId").AsInt32().PrimaryKey("PK_roles").Indexed("IX_roles_UserId")
                    .ForeignKey("FK_roles_user_UserId", DatabaseNames.UsersSchemaName, DatabaseNames.UserTableName, "Id")
                .WithColumn("RoleId").AsInt32().PrimaryKey("PK_roles").Indexed("IX_roles_RoleId")
                    .ForeignKey("FK_roles_role_RoleId", DatabaseNames.UsersSchemaName, DatabaseNames.RoleTableName, "Id")
                    .OnDelete(System.Data.Rule.Cascade);
        }
    }
}
