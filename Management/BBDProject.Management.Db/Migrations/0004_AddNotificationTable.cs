using FluentMigrator;

namespace BBDProject.Management.Db.Migrations
{
    [Migration(0004)]
    public class AddNotificationTable : Migration
    {
        private string _tableName = "notification";
        public override void Down()
        {
            Delete.Table(_tableName);
        }

        public override void Up()
        {
            Create.Table(_tableName)
                .WithColumn("id").AsInt32().PrimaryKey($"PK_{_tableName}").Identity()
                .WithColumn("id_role").AsInt32().NotNullable()
                .WithColumn("content").AsString().NotNullable()
                .WithColumn("date_added").AsDateTime().NotNullable();

            Create.ForeignKey("FK_role_notification")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_role")
                .ToTable("role").InSchema(DatabaseNames.UsersSchemaName).PrimaryColumn("Id");
        }
    }
}
