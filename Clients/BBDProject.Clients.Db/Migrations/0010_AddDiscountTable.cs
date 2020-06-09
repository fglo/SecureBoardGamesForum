using FluentMigrator;

namespace BBDProject.Clients.Db.Migrations
{
    [Migration(0010)]
    public class AddDiscountTable : Migration
    {
        private string _tableName = "discount";
        public override void Down()
        {
            Delete.Table(_tableName);
        }

        public override void Up()
        {
            Create.Table(_tableName)
                .WithColumn("id").AsInt32().PrimaryKey($"PK_{_tableName}").Identity()
                .WithColumn("cost").AsInt32().NotNullable()
                .WithColumn("discount_percent").AsInt32().NotNullable()
                .WithColumn("date_added").AsDateTime().NotNullable()
                .WithColumn("deleted").AsBoolean().NotNullable().WithDefaultValue(false);
        }
    }
}
