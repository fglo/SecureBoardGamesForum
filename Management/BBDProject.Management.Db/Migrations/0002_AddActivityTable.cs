using FluentMigrator;

namespace BBDProject.Management.Db.Migrations
{
    [Migration(0002)]
    public class AddActivityTable : Migration
    {
        private string _tableName = "activity";
        public override void Down()
        {
            Delete.Table(_tableName);
        }

        public override void Up()
        {
            Create.Table(_tableName)
                .WithColumn("id").AsInt32().PrimaryKey($"PK_{_tableName}").Identity()
                .WithColumn("table_name").AsString().NotNullable()
                .WithColumn("activity_name").AsString().NotNullable();
        }
    }
}
