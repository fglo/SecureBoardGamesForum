using FluentMigrator;

namespace BBDProject.Clients.Db.Migrations
{
    [Migration(0002)]
    public class AddProductTable : Migration
    {
        private string _tableName = "product";
        public override void Down()
        {
            Delete.Table(_tableName);
        }

        public override void Up()
        {
            Create.Table(_tableName)
                .WithColumn("id").AsInt32().PrimaryKey($"PK_{_tableName}").Identity()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("brand").AsString().NotNullable()
                .WithColumn("model").AsString().NotNullable()
                .WithColumn("date_added").AsDateTime().NotNullable();
        }
    }
}
