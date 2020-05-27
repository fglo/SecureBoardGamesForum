using FluentMigrator;

namespace BBDProject.Management.Db.Migrations
{
    [Migration(0003)]
    public class AddUserActivityTable : Migration
    {
        private string _tableName = "user_activity";
        public override void Down()
        {
            Delete.Table(_tableName);
        }

        public override void Up()
        {
            Create.Table(_tableName)
                .WithColumn("id").AsInt32().PrimaryKey($"PK_{_tableName}").Identity()
                .WithColumn("id_user").AsInt32().NotNullable()
                .WithColumn("id_activity").AsInt32().NotNullable()
                .WithColumn("date_added").AsDateTime().NotNullable();

            Create.ForeignKey("FK_user_user_activity")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_user")
                .ToTable("user").InSchema("users").PrimaryColumn("Id");

            Create.ForeignKey("FK_activity_user_activity")
                .FromTable(_tableName).InSchema("public").ForeignColumn("id_activity")
                .ToTable("activity").InSchema("public").PrimaryColumn("id");
        }
    }
}
